using AutoMapper;
using Azure.Core;
using BusinessLayer.Abstraction;
using BusinessLayer.Helpers;
using DomainLayer.DTOs.UserDtos;
using DomainLayer.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Abstraction.UserInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Concrete.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> userRepo;
        private readonly IRepository<Role> roleRepo;
        private readonly IRepository<UserRole> userRoleRepo;
        private readonly TokenManagement tokenManagement;
        public UserService(IMapper Mapper, 
            IRepository<User> UserRepo , 
            IRepository<Role> RoleRepo, 
            IRepository<UserRole> UserRoleRepo,
            IOptions<TokenManagement> TokenManagement
            )
        {
            mapper = Mapper;
            userRepo = UserRepo;
            roleRepo = RoleRepo;
            userRoleRepo = UserRoleRepo;
            tokenManagement = TokenManagement.Value;
        }
        public Role AddRole(string RoleName)
        {
            Role role = new Role();
            role.RoleName=RoleName;
            role.CreatedAt=DateTime.Now;
            roleRepo.Add(role);
            roleRepo.Save();
            return role;
        }

        public User AddUser(AddUserDTO UserObj)
        {
            var User = mapper.Map<User>(UserObj);
            User.CreatedAt = DateTime.Now;
            User.Password = EncryptANDDecrypt.EncryptText(UserObj.Password);
            userRepo.Add(User);
            userRepo.Save();

            return User;
        }

        public bool AddUserToRole(AddUserToRoleDto UserRoleObj)
        {
            var UserRole = mapper.Map<UserRole>(UserRoleObj);
            UserRole.CreatedAt = DateTime.Now;
            userRoleRepo.Add(UserRole);
            userRoleRepo.Save();

            return true;
        }

        public Role GetRole(Guid RoleId)
        {
           return roleRepo.GetById(RoleId);
        }

        public IQueryable<Role> GetRoles()
        {
            return roleRepo.GetAll();
        }

        public IQueryable<User> GetRoleUsers(Guid RoleId)
        {
            var AllUser = userRoleRepo.GetMany(a=>a.RoleId==RoleId).Select(a=>a.User);
            return AllUser;
        }

        public User GetUser(Guid UserId)
        {
           return userRepo.GetById(UserId);
        }

        public Role GetUserRole(Guid UserId)
        {
            var User = userRoleRepo.GetMany(a=>a.UserId==UserId).Include(a=>a.User).FirstOrDefault();
            return User?.Role;
        }

        public IQueryable<User> GetUsers()
        {
            return userRepo.GetAll();
        }

        public LoginResultDto Login(LoginDto LoginDto)
        {
            var EncryptedPass = EncryptANDDecrypt.EncryptText(LoginDto.Password);
            var User = userRepo.GetMany(a=>a.UserName==LoginDto.UserName&&a.Password==EncryptedPass).FirstOrDefault();
            if (User!=null)
            {

                //Generate Token For The User
                List<Claim> ClaimList = new List<Claim>();

                var UserRole = userRoleRepo.GetMany(a => a.UserId == User.Id).Include(a => a.Role).FirstOrDefault();
                ClaimList.Add(new Claim(ClaimTypes.Role, UserRole.Role.RoleName));
                ClaimList.Add(new Claim(ClaimTypes.Name, User.UserName));
                ClaimList.Add(new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var expireDate = DateTime.Now.AddMinutes(tokenManagement.AccessExpiration);
                ClaimList.Add(new Claim(ClaimTypes.DateOfBirth, expireDate.ToString()));
                var tokenDiscriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(ClaimList),
                    Expires = expireDate,
                    SigningCredentials = credentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenObj = tokenHandler.CreateToken(tokenDiscriptor);
              var   token = tokenHandler.WriteToken(tokenObj);

                return new LoginResultDto { Expire =DateTime.Now.AddHours(tokenManagement.AccessExpiration) ,User=User,Token= token };

            }
            return new LoginResultDto { Expire =DateTime.Now ,User=null,Token="No Token" };
        }
    }
}
