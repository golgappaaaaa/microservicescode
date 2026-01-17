using Microsoft.AspNetCore.Mvc;

namespace DomainBApi.Controllers
{
    [ApiController]
    [Route("api/receive")]
    public class ReceiveController : ControllerBase
    {
        [HttpGet]
        public IActionResult ReceiveFromQuery([FromQuery] string name, [FromQuery] int age)
        {
            return Ok($"Received via query: {name}, {age}");
        }

        [HttpPost]
        public IActionResult ReceiveForm([FromForm] Person person)
        {
            return Ok($"Received from form: {person.Name}, {person.Age}");
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
