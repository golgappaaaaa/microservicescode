using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers;

[ApiController]
[Route("api/customers")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new[] { "Customer A", "Customer B" });

    [HttpGet("test-deploy")]
    public IActionResult CheckautomaticdeplomenttillrenderGet()
    {
        return Ok("CheckautomaticdeplomenttillrenderGet A");
    }
}
