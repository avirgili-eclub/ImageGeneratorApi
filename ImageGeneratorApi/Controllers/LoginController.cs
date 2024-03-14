using System.Security.Claims;
using System.Text;
using ImageGeneratorApi.Core.Login.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ImageGeneratorApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    /// <summary>
    /// Login method to generate a token.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Login(UserLogin login)
    {
        try
        {
            if (string.IsNullOrEmpty(login.UserName) ||
                string.IsNullOrEmpty(login.Password))
                return BadRequest("Username and/or Password cannot be empty.");

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest
                ("An error occurred in generating the token");
        }
    }
}