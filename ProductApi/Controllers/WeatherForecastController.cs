using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/products")]
public class WeatherForecastController : ControllerBase
{
    [Authorize]
    [HttpGet]
    // Protect this route with JWT Authentication
    public IActionResult Get() => Ok(new[] { "Product 1", "Product 2" });
}

