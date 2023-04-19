using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PseudoCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Test");
        }
    }
}
