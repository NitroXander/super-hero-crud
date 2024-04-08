using Microsoft.EntityFrameworkCore;
using SuperHeros.DTOs;
using SuperHeros.DTOs.Requests;
using SuperHeros.DTOs.Responces;
using SuperHeros.Helpers.Utils;
using SuperHeros.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SuperHeros.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public BaseResponce Authenticate(UserLoginRequest request)
        {
            BaseResponce responce;

            try
            {
                UserModel? user = context.Users.Where(user => user.email == request.email).FirstOrDefault();

                if (user == null)
                {
                    return new BaseResponce
                    {
                        status = StatusCodes.Status401Unauthorized,
                        data = new { message = "Invalid Email or Password" }
                    };
                }

                string md5Password = Supports.GetMd5HashedOutput(request.password);

                if (user.password == md5Password)
                {
                    string jwt = JwtUtils.GenerateJwtToken(user);

                    LoginDetailsModel? loginDetails = context.LoginDetails.Where(loginDetails => loginDetails.id == user.id).FirstOrDefault();

                    if (loginDetails == null)
                    {
                        loginDetails = new LoginDetailsModel();
                        loginDetails.id = user.id;
                        loginDetails.email = request.email;

                        if (user.role != null)
                        {
                            RoleModel? role = context.Roles.Where(role => role.id == user.role).FirstOrDefault();
                            loginDetails.role = role.role_name;
                            loginDetails.permissions = role.permissions;
                        }

                        loginDetails.jwt_token = jwt;

                        context.LoginDetails.Add(loginDetails);
                    }
                    else
                    {
                        loginDetails.jwt_token = jwt;
                    }

                    context.SaveChanges();

                    responce = new BaseResponce
                    {
                        status = StatusCodes.Status200OK,
                        data = new { token = jwt }
                    };

                    return responce;
                }
                else
                {
                    return new BaseResponce
                    {
                        status = StatusCodes.Status401Unauthorized,
                        data = new { message = "Invalid Email or Password" }
                    };
                }
            }
            catch (Exception ex)
            {
                responce = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error" + ex.Message }
                };

                return responce;
            }

        }

        public BaseResponce CreateUser(CreateUserRequest request)
        {
            BaseResponce responce;

            try
            {
                UserModel newUser = new UserModel
                {
                    first_name = request.first_name,
                    last_name = request.last_name,
                    email = request.email,
                    country_code = request.country_code,
                    phone_number = request.phone_number,
                    country = request.country,
                    state = request.state,
                    city = request.city,
                    address = request.address,
                    password = Supports.GetMd5HashedOutput(request.password),
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,

                };

                context.Add(newUser);
                context.SaveChanges();

                responce = new BaseResponce
                {
                    status = StatusCodes.Status200OK,
                    data = new { message = "User created successfully" }
                };

                return responce;
            }
            catch (Exception ex)
            {
                responce = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error" }
                };

                return responce;
            }
        }

        public BaseResponce UpdateUser(string email, UpdateUserRequest request)
        {
            BaseResponce responce;

            try
            {
                using (context)
                {
                    UserModel findUser = context.Users.Where(user => user.email == email).FirstOrDefault();

                    if (findUser != null)
                    {
                        findUser.first_name = request.first_name;
                        findUser.last_name = request.last_name;
                        findUser.email = request.email;
                        findUser.country_code = request.country_code;
                        findUser.phone_number = request.phone_number;
                        findUser.country = request.country;
                        findUser.state = request.state;
                        findUser.city = request.city;
                        findUser.address = request.address;
                        findUser.role = request.role;
                        findUser.password = Supports.GetMd5HashedOutput(request.password);
                        findUser.created_at = DateTime.Now;
                        findUser.updated_at = DateTime.Now;

                        context.SaveChanges();
                        responce = new BaseResponce
                        {
                            status = StatusCodes.Status200OK,
                            data = new { message = "User created successfully" }
                        };
                    }
                    else
                    {
                        responce = new BaseResponce
                        {
                            status = StatusCodes.Status400BadRequest,
                            data = new { message = "User not found" }
                        };
                    }

                }
            }
            catch (Exception ex)
            {
                responce = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error" }
                };

            }
            return responce;
        }

        public BaseResponce FindUserById(long id)
        {
            BaseResponce responce;

            try
            {
                UserModel user = context.Users.Where(user => user.id == id).FirstOrDefault();

                if (user != null)
                {
                    responce = new BaseResponce
                    {
                        status = StatusCodes.Status400BadRequest,
                        data = new { message = user }
                    };
                }
                else
                {
                    responce = new BaseResponce
                    {
                        status = StatusCodes.Status400BadRequest,
                        data = new { message = "User Not Found" }
                    };
                }
                return responce;
            }
            catch (Exception ex)
            {
                responce = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error" }
                };

            }
            return responce;
        }

        public BaseResponce FindUserPermissionsById(long id)
        {
            BaseResponce responce;

            try
            {
                UserModel user = context.Users.Where(user => user.id == id).FirstOrDefault();

                if (user != null)
                {
                    RoleModel role = context.Roles.Where(role => role.id == user.role).FirstOrDefault();
                    if (role != null)
                    {
                        responce = new BaseResponce
                        {
                            status = StatusCodes.Status200OK,
                            data = new { role.permissions }
                        };
                    }
                    else
                    {
                        responce = new BaseResponce
                        {
                            status = StatusCodes.Status400BadRequest,
                            data = new { message = "No Role Found for the user" }
                        };
                    }
                }
                else
                {
                    responce = new BaseResponce
                    {
                        status = StatusCodes.Status400BadRequest,
                        data = new { message = "User Not Found" }
                    };
                }
                return responce;
            }
            catch (Exception ex)
            {
                responce = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error" }
                };

            }
            return responce;
        }

        public BaseResponce UpdateUserRoleByUserId(long id, UpdateRoleRequest request)
        {
            BaseResponce responce;

            try
            {
                UserModel user = context.Users.Where(user => user.id == id).FirstOrDefault();
                if (user != null)
                {
                    RoleModel role = context.Roles.Where(role => role.id == user.role).FirstOrDefault();
                    if (role != null)
                    {
                        role.role_name = request.role_name;
                        role.role_description = request.role_description;
                        role.permissions = request.permissions;

                        context.SaveChanges();

                        responce = new BaseResponce
                        {
                            status = StatusCodes.Status200OK,
                            data = new { message = "Role Updated Successfully" }
                        };
                    }
                    else
                    {
                        responce = new BaseResponce
                        {
                            status = StatusCodes.Status400BadRequest,
                            data = new { message = "Role Not Found" }
                        };
                    }
                }
                else
                {
                    responce = new BaseResponce
                    {
                        status = StatusCodes.Status400BadRequest,
                        data = new { message = "User Not Found" }
                    };
                }
                return responce;
            }
            catch (Exception ex)
            {
                responce = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error" }
                };

            }
            return responce;
        }

        public BaseResponce updateRoleByUserIdJoinQuery(long id, UpdateRoleRequest request)
        {
            BaseResponce responce;

            

            try
            {


                RoleModel role = getUser(id).First();

                if (role != null)
                {
                    role.role_name = request.role_name;
                    role.role_description = request.role_description;
                    role.permissions = request.permissions;
                }

                context.SaveChanges();

                responce = new BaseResponce
                {
                    status = StatusCodes.Status200OK,
                    data = new { message = "Role Updated Successfully" }
                };

            }
            catch (Exception ex)
            {
                responce = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error" }
                };

            }
            return responce;
        }

        public List<RoleModel> getUser(long id)
        {
            List<RoleModel> selectedRoles = context.Roles.Join(context.Users,
              r => r.id,
              u => u.role,
              (r, u) => new
              {
                  r,u
              })
              .Where(role => role.u.id == id).Select(role=> role.r)
              .ToList();

            return selectedRoles;

        }
    }
}
