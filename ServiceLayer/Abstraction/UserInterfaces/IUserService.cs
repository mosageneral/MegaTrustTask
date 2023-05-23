using DomainLayer.DTOs.UserDtos;
using DomainLayer.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Abstraction.UserInterfaces
{
    public interface IUserService
    {
        public User AddUser(AddUserDTO UserObj); 
        public Role AddRole(string RoleName); 
        public bool AddUserToRole(AddUserToRoleDto UserRoleObj);
        public IQueryable<User> GetUsers();
        public IQueryable<Role> GetRoles();
        public User GetUser(Guid UserId);
        public Role GetRole(Guid RoleId);
        public Role GetUserRole(Guid UserId);
        public IQueryable<User> GetRoleUsers(Guid RoleId);
        public LoginResultDto Login(LoginDto LoginDto);

    }
}
