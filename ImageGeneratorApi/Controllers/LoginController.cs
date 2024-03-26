using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ImageGeneratorApi.Core.Login.Dto;
using ImageGeneratorApi.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ImageGeneratorApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public LoginController(SignInManager<User> signInManager, UserManager<User> userManager,
        IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Login method to generate a token.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost]
    
    public async Task<IActionResult> Login([FromBody] UserLogin login)
    {
        try
        {
            if (string.IsNullOrEmpty(login.UserName) ||
                string.IsNullOrEmpty(login.Password))
                return BadRequest("Username and/or Password cannot be empty.");

            var user = await _userManager.FindByNameAsync(login.UserName);
            
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password)) 
                return Unauthorized();
            
            var token = GenerateJwtToken(user);
            return Ok(new { token });
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest
                ("An error occurred in generating the token");
        }
    }

    //TODO: Mover este metodo a un servicio.
    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = _configuration["JwtSettings:SecretKey"];

        var key = Encoding.UTF8.GetBytes(secretKey!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
                // new Claim(ClaimTypes.Actor, "http://localhost:5228"),
                // new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddHours(1).ToString())
                // agregar más claims según sea necesario
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Tiempo de expiración del token
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = "UMBRELLA",
            Audience = "http://localhost:5228" // Establece la audiencia del token
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}