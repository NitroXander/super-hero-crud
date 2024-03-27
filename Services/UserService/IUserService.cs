using SuperHeros.DTOs.Requests;
using SuperHeros.DTOs.Responces;

namespace SuperHeros.Services.UserService
{
    public interface IUserService
    {
        BaseResponce CreateUser(CreateUserRequest request);

        BaseResponce Authenticate(UserLoginRequest request);

        BaseResponce UpdateUser(string email, UpdateUserRequest request);

        BaseResponce FindUserPermissionsById(long id);

        BaseResponce UpdateUserRoleByUserId(long id, UpdateRoleRequest request);
    }
}
