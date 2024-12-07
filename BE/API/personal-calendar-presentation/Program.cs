using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Services;
using personal_calendar_application.Users.Commands.Create;
using personal_calendar_infrastructure.Database;
using personal_calendar_infrastructure.Repositories;

// var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
{
    // var jwtConfig = builder.Configuration.GetSection("JwtSettings").Get<JwtConfig>();
    // builder.Services.AddSingleton(jwtConfig!);
    var config = builder.Configuration; builder.Services.AddCors(options =>
    {
        options.AddPolicy("React-app",
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                          });
    });
    // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    //     x => x.TokenValidationParameters = new TokenValidationParameters
    //     {
    //         ValidIssuer = config["JwtSettings:Issuer"],
    //         ValidAudience = config["JwtSettings:Audience"],
    //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
    //         ValidateIssuer = true,
    //         ValidateAudience = true,
    //         ValidateLifetime = true,
    //         ValidateIssuerSigningKey = true
    //     }
    // );
    builder.Services.AddAuthorization();
    builder.Services.AddControllers();
    builder.Services.AddDbContext<CalendarDbContext>(options =>
        options.UseSqlite("Data Source=CalendarAPI.db"));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IEventRepository, EventRepository>();
    builder.Services.AddScoped<IEventSharingService, EventSharingService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddSingleton<IHashService, HashService>();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));
}


var app = builder.Build();
{
    // app.UseRouting();
    app.UseHttpsRedirection();
    app.UseCors("React-app");
    // app.UseAuthentication();
    // app.UseAuthorization();
    app.MapControllers();
    app.Run();
}






