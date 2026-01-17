using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DomainAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainAController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostDataToDomainB()
        {
            var httpClient = new HttpClient();
            var data = new { Name = "John", Age = 30 };

            var response = await httpClient.PostAsJsonAsync("https://localhost:5001/api/receive", data);

            return Ok(await response.Content.ReadAsStringAsync());
        }

        public IActionResult RedirectToDomainB()
        {
            var name = "John";
            var age = 30;
            return Redirect($"https://domainb.com/api/receive?name={name}&age={age}");
        }
    }
}
