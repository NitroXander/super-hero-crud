using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeros.DTOs.Requests;
using SuperHeros.DTOs.Responces;
using SuperHeros.Services.HeroService;
using SuperHeros.Services.UserService;

namespace SuperHeros.Controllers
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

        [HttpPost("createNewUser")]
        public BaseResponce CreateUser(CreateUserRequest request)
        {
            return userService.CreateUser(request);
        }

        [HttpPut("updateUser/{email}")]
        public BaseResponce UpdateUser(string email, UpdateUserRequest request)
        {
            return userService.UpdateUser(email, request);
        }

        [HttpPost("login")]
        public BaseResponce Authenticate(UserLoginRequest request)
        {
            return userService.Authenticate(request);
        }

        [HttpGet("getUserPermissionsById/{id}")]
        public BaseResponce GetUserPermissionsById(long id)
        {
            return userService.FindUserPermissionsById(id);
        }

        [HttpPut("UpdateUserRoleByUserId/{id}")]
        public BaseResponce UpdateUserRoleByUserId(long id, UpdateRoleRequest request)
        {
            return userService.UpdateUserRoleByUserId(id, request);
        }
    }
}
