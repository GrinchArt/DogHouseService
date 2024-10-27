using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogHouseService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {

        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Dogshouseservice.Version1.0.1");
        }
    }
}
