using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
            
        }

        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("Test Inbound Connection");
            return Ok("Test Inbound Connection");
        }
    }
}
