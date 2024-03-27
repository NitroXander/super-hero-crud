using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeros.DTOs.Requests;
using SuperHeros.DTOs.Responces;
using SuperHeros.Services.RoleService;

namespace SuperHeros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost("addCreatedRole")]
        public BaseResponce CreateRole(CreateRoleRequest request)
        {
            return roleService.CreateRole(request);
        }

        [HttpGet("listAvailableRoles")]
        public BaseResponce RoleList()
        {
            return roleService.RoleList();
        }

        [HttpGet("searchRoleById/{id}")]
        public BaseResponce GetRoleById(long id)
        {
            return roleService.GetRoleById(id);
        }


        [HttpDelete("deleteRoleById/{id}")]
        public BaseResponce DeleteRoleById(long id)
        {
            return roleService.DeleteRoleById(id);
        }

        [HttpPut("updateRoleById/{id}")]
        public BaseResponce UpdateRoleById(long id, UpdateRoleRequest request)
        {
            return roleService.UpdateRoleById(id, request);
        }
    }
}
