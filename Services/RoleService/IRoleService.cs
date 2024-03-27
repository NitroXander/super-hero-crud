using SuperHeros.DTOs.Requests;
using SuperHeros.DTOs.Responces;

namespace SuperHeros.Services.RoleService
{
    public interface IRoleService
    {
        BaseResponce CreateRole(CreateRoleRequest request);

        BaseResponce RoleList();

        BaseResponce GetRoleById(long id);

        BaseResponce UpdateRoleById(long id, UpdateRoleRequest request);

        BaseResponce DeleteRoleById(long id);
    }
}
