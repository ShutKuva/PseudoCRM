using BusinessLogicLayer.Abstractions.Auth;
using Core.Auth.Jwt.Parameters;
using Core.Auth.Jwt.Results;
using Core.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace PseudoCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
    {
        private readonly IAuthService<JwtAuthLoginParameters, JwtAuthRegistrationParameters, JwtResult, JwtResult> _authService;

        public JwtAuthController(IAuthService<JwtAuthLoginParameters, JwtAuthRegistrationParameters, JwtResult, JwtResult> _authService)
        {
            this._authService = _authService;
        }

        [HttpGet("login")]
        public async Task<ActionResult<JwtResult>> Login([FromBody] UserLoginDto user)
        {
            return Ok(await _authService.Login(new JwtAuthLoginParameters{ User = user }));
        }

        [HttpGet("refresh")]
        public async Task<ActionResult<JwtResult>> RefreshToken([FromBody] JwtResult oldResult)
        {
            return Ok(await _authService.Login(new JwtAuthLoginParameters{ OldResult = oldResult }));
        }

        [HttpPost("register")]
        public async Task<ActionResult<JwtResult>> Register([FromBody] UserRegistrationDto user)
        {
            return Ok(await _authService.Register(new JwtAuthRegistrationParameters{User = user}));
        }
    }
}
