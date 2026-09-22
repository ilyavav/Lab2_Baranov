using Microsoft.AspNetCore.Mvc;
using Lab2_Baranov.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpGet("token")]
    public object GetToken()
    {
        return AuthOptions.GenerateToken();
    }

    [HttpGet("token/secret")]
    public object GetAdminToken()
    {
        return AuthOptions.GenerateToken(true);
    }
}