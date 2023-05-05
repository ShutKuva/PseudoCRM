using BusinessLogicLayer.Abstractions.Chat.Facades;
using Core.Auth.Jwt.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace PseudoCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMessageFacade _messageFacade;

        public ChatController(IMessageFacade messageFacade)
        {
            _messageFacade = messageFacade;
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages()
        {
            int parsedId = int.Parse(User.FindFirst(claim => claim.Type == ClaimNames.Id).Value);

            return Ok(await _messageFacade.GetMessagesByUserIdAsync(parsedId));
        }
    }
}
