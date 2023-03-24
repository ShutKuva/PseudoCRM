using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PseudoCrmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
