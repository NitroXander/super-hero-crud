using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SuperHeros.DTOs.Responces;
using SuperHeros.Helpers.Utils;
using System.Threading.Tasks;

namespace SuperHeros.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string? token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];

            if(token == null)
            {
                if(IsEnabledUnauthorizedRoute(httpContext))
                {
                    return _next(httpContext);
                }

                BaseResponce responce = new BaseResponce{
                    status = StatusCodes.Status401Unauthorized,
                    data = new { message = "Unauthorized" } 
                };
                httpContext.Response.StatusCode =responce.status;
                httpContext.Response.ContentType = "application/json";
                return httpContext.Response.WriteAsJsonAsync(responce);
            }
            else
            {
                if(JwtUtils.ValidateJwtToken(token))
                    {
                    return _next(httpContext);
                }
                else
                {
                    BaseResponce responce = new BaseResponce
                    {
                        status = StatusCodes.Status401Unauthorized,
                        data = new { message = "Unauthorized" }
                    };
                    httpContext.Response.StatusCode = responce.status;
                    httpContext.Response.ContentType = "application/json";
                    return httpContext.Response.WriteAsJsonAsync(responce);
                }
            }
        }

        private bool IsEnabledUnauthorizedRoute(HttpContext context)
        {
            List<string> enabledRoutes = new List<string>
            {
                "/api/User/createNewUser",
                "/api/User/login",
                "/api/Hero/listAvailableHeros",
                "/api/Hero/searchHeroByName/{string}"
            };

            bool isEnableUnauthorizedRoute = false;

            if (context.Request.Path.Value is not null)
            {
                isEnableUnauthorizedRoute = enabledRoutes.Contains(context.Request.Path.Value);
            }

             return isEnableUnauthorizedRoute;
        }
    }


    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
