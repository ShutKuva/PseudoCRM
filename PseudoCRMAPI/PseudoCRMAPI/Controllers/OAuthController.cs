using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PseudoCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("{scheme}/login")]
        public IActionResult Login(string scheme)
        {
            AuthenticationProperties ao = new AuthenticationProperties();
            ao.RedirectUri = "";
            return Challenge(ao, scheme);
        }
    }
}
