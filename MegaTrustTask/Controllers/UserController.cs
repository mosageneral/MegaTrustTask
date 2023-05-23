using BusinessLayer.Helpers;
using DomainLayer.DTOs.UserDtos;
using DomainLayer.Entities.UserEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Abstraction.UserInterfaces;
using System.Security.Claims;

namespace PresentaionLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet,Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(userService.GetUsers());
        }
        [HttpGet,Route("GetUser")]
        public IActionResult GetUser(Guid id)
        {
            return Ok(userService.GetUser(id));
        }
        [HttpPost,Route("AddUser")]
        public IActionResult AddUser(AddUserDTO obj)
        {
           
            return Ok(userService.AddUser(obj));
        }
        [HttpPost,Route("AddRole")]
        public IActionResult AddRole(string RoleName)
        {
            return Ok(userService.AddRole(RoleName));
        }
        [HttpGet,Route("GetRole")]
        public IActionResult GetRole(Guid id)
        {
            return Ok(userService.GetRole(id));
        }
        [HttpGet,Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            return Ok(userService.GetRoles());
        }
        [HttpGet,Route("GetUserRole")]
        public IActionResult GetUserRole(Guid UserId)
        {
            return Ok(userService.GetUserRole(UserId));
        }
        [HttpPost,Route("AddUserToRole")]
        public IActionResult AddUserToRole(AddUserToRoleDto obj)
        {
            return Ok(userService.AddUserToRole(obj));
        }
        [ClaimRequirement(ClaimTypes.Role, "Admin")]
        [HttpGet,Route("TestAdminRole")]
        public IActionResult TestAdminRole()
        {
            return Ok();
        }
        [ClaimRequirement(ClaimTypes.Role, "Employee")]
        [HttpGet, Route("TestEmployeeRole")]
        public IActionResult TestEmployeeRole()
        {
            return Ok();
        }

        [HttpPost ,Route("Login")]
        public IActionResult Login(LoginDto login)
        {
            return Ok(userService.Login(login));
        }


    }
}
