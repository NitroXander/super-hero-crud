using SuperHeros;
using Microsoft.EntityFrameworkCore;
using SuperHeros.Services.HeroService;
using SuperHeros.Services.UserService;
using Microsoft.OpenApi.Models;
using SuperHeros.Services.RoleService;
using SuperHeros.Helpers.Utils.GlobalAttributes;
using SuperHeros.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add global Attributes 
GlobalAttributes.mySqlConnection.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.


// Add Application DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // here we get the connection string which is defined in the appsettings.Development.json file and connect it to builder using the constructor created in ApplicationDbContext
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Example API",
        Version = "v1",
        Description = "An example of an ASP.NET Core Web API",
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Email = "example@example.com",
            Url = new Uri("https://example.com/contact"),
        },
    });

});

// Register Hero Services
builder.Services.AddScoped<IHeroService , HeroService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseJwtMiddleware();

app.MapControllers();

app.Run();
