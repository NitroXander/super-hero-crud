using SuperHeros.DTOs;
using SuperHeros.DTOs.Requests;
using SuperHeros.DTOs.Responces;
using SuperHeros.Models;

namespace SuperHeros.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext context;

        public RoleService(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public BaseResponce CreateRole(CreateRoleRequest request)
        {
            BaseResponce response;

            try
            {
                RoleModel newRole = new RoleModel
                {
                    role_name = request.role_name,
                    role_description = request.role_description,
                    permissions = request.permissions,
                };



                context.Add(newRole);
                context.SaveChanges();

                response = new BaseResponce
                {
                    status = StatusCodes.Status200OK,
                    data = new { message = "Role created successfully" }
                };

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponce RoleList()
        {
            BaseResponce response;

            try
            {
                List<RoleDTO> roles = new List<RoleDTO>();

                using (context)
                {
                    context.Roles.ToList().ForEach(role => roles.Add(new RoleDTO
                    {
                        id = role.id,
                        role_name = role.role_name,
                        role_description = role.role_description,
                        permissions = role.permissions,
                    }));
                }

                response = new BaseResponce
                {
                    status = StatusCodes.Status200OK,
                    data = new { roles }
                };

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponce GetRoleById(long id)
        {
            BaseResponce response;

            try
            {
                RoleDTO role = new RoleDTO();

                using (context)
                {
                    RoleModel findRole = context.Roles.Where(role => role.id == id).FirstOrDefault();

                    if (findRole != null)
                    {
                        role.id = findRole.id;
                        role.role_name = findRole.role_name;
                        role.role_description = findRole.role_description;
                        role.permissions = findRole.permissions;
                    }
                    else
                    {
                        role = null;
                    }
                }

                if (role != null)
                {
                    response = new BaseResponce
                    {
                        status = StatusCodes.Status200OK,
                        data = new { role }
                    };
                }
                else
                {
                    response = new BaseResponce
                    {
                        status = StatusCodes.Status404NotFound,
                        data = new { message = "No Role Found for given Id" }
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Interal Server Error : " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponce UpdateRoleById(long id, UpdateRoleRequest role)
        {
            BaseResponce response;

            try
            {
                using (context)
                {
                    RoleModel findRole = context.Roles.Where(role => role.id == id).FirstOrDefault();

                    if (findRole != null)
                    {
                        findRole.role_name = role.role_name;
                        findRole.role_description = role.role_description;
                        findRole.permissions = role.permissions;

                        context.SaveChanges();

                        response = new BaseResponce
                        {
                            status = StatusCodes.Status200OK,
                            data = new { message = "Role updated successfully" }
                        };

                    }
                    else
                    {
                        response = new BaseResponce
                        {
                            status = StatusCodes.Status404NotFound,
                            data = new { message = "Role Not Found" }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };

            }
            return response;
        }

        public BaseResponce DeleteRoleById(long id)
        {
            BaseResponce response;

            try
            {
                using (context)
                {
                    RoleModel findRole = context.Roles.Where(role => role.id == id).FirstOrDefault();

                    if (findRole != null)
                    {
                        context.Roles.Remove(findRole);
                        context.SaveChanges();

                        response = new BaseResponce
                        {
                            status = StatusCodes.Status200OK,
                            data = new { message = "Role deleted successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponce
                        {
                            status = StatusCodes.Status404NotFound,
                            data = new { message = "Role Not Found" }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };

            }
            return response;
        }
    }
}
