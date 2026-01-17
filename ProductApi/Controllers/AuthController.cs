using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Simple validation
        if (request.Username == "admin" && request.Password == "password")
        {
            // EXACT same key string jo Gateway aur ProductApi ke Program.cs mein hai
            var keyStr = "Kar@9871_Aapki_Secret_Key_32_Chars!";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iss, "karan"), // Explicitly adding issuer
                    new Claim(JwtRegisteredClaimNames.Aud, "karan")  // Explicitly adding audience
                };

            var token = new JwtSecurityToken(
                issuer: "karan",
                audience: "karan",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        return Unauthorized();
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
