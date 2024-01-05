using Microsoft.AspNetCore.Mvc;

namespace SeventhCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : Controller
    {
        [HttpGet]
        public ActionResult<string> Hello()
        {
            return Ok("Hello! I'm alive!!!");
        }
    }
}