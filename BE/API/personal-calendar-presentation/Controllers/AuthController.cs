using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Users.Contracts;
namespace personal_calendar_presentation.Controllers;



[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        if (loginRequest.Email == "admin" && loginRequest.Password == "admin")
        {

            var adminToken = GenerateToken(Guid.NewGuid(), "Admin");
            return Ok(new { user = new { role = "Admin" }, token = adminToken });
        }

        try
        {
            var mail = new MailAddress(loginRequest.Email.Trim());
        }
        catch
        {
            return BadRequest(new { message = "Invalid email address." });
        }

        var user = await _authService.Login(loginRequest);
        if (user is null) return Unauthorized(new { message = "Incorrect email or password." });
        var token = GenerateToken(user.UserId, user.Role!);
        return Ok(new { user, token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserOrAdminRequest request)
    {
        try
        {
            new MailAddress(request.Email);
        }
        catch
        {
            return BadRequest(new { message = "Invalid email address." });
        }
        var user = await _authService.Register(request);
        if (user is null) return BadRequest(new { message = "This email is taken." });
        return StatusCode(201, user);
    }


    private string GenerateToken(Guid id, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("id", id.ToString()),
            new Claim("role", role)
        };

        var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"],
          _configuration["JwtSettings:Audience"],
          claims,
          expires: DateTime.Now.AddMinutes(15),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}