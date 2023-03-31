using BusinessLogicLayer.Abstractions.Email;
using Core;
using Core.Auth.Jwt.Parameters;
using Core.Email;
using Core.Email.Additional;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using Core.Dtos;

namespace PseudoCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService<string, EmailCredentials> _emailService;
        private readonly IMessageReceiver<IReadOnlyList<EmailTextMessage>, string, string, ServerProtocols> _sharedMessageReceiver;
        private readonly IMessageReceiver<IReadOnlyList<EmailTextMessage>, string, string, SearchQuery> _imapMessageReceiver;
        private readonly IMessageReceiver<IReadOnlyList<EmailTextMessage>, string, string, int> _popMessageReceiver;
        private readonly IMessageSender<string, string, EmailTextMessage> _messageSender;

        public EmailController(
            IEmailService<string, EmailCredentials> emailService,
            IMessageReceiver<IReadOnlyList<EmailTextMessage>, string, string, ServerProtocols> sharedMessageReceiver,
            IMessageReceiver<IReadOnlyList<EmailTextMessage>, string, string, SearchQuery> imapMessageReceiver,
            IMessageReceiver<IReadOnlyList<EmailTextMessage>, string, string, int> popMessageReceiver,
            IMessageSender<string, string, EmailTextMessage> messageSender)
        {
            _emailService = emailService;
            _sharedMessageReceiver = sharedMessageReceiver;
            _imapMessageReceiver = imapMessageReceiver;
            _popMessageReceiver = popMessageReceiver;
            _messageSender = messageSender;
        }

        [HttpGet("public-names")]
        public async Task<ActionResult<IEnumerable<EmailDto>>> GetPublicNames()
        {
            return Ok(await _emailService.GetRegisteredPublicNames(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value));
        }

        [HttpPost("set-email")]
        public async Task<OkResult> SetNewEmailCredentials([FromBody] EmailCredentials emailCredentials)
        {
            await _emailService.SetNewEmail(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value,
                emailCredentials);
            return Ok();
        }

        [HttpGet("pop/messages/{publicName}/{getLast}")]
        public async Task<ActionResult<IReadOnlyList<EmailTextMessage>>> GetMessageWithPop(string publicName, int getLast)
        {
            return Ok(await _popMessageReceiver.GetMessages(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value, publicName, getLast));
        }

        [HttpGet("imap/messages/{publicName}")]
        public async Task<ActionResult<IReadOnlyList<EmailTextMessage>>> GetMessageWithImap(string publicName)
        {
            return Ok(await _sharedMessageReceiver.GetMessages(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value, publicName, ServerProtocols.Imap));
        }

        [HttpPost("send/{publicName}")]
        public async Task<ActionResult<OkObjectResult>> SendMessage(string publicName, [FromBody]EmailTextMessage emailTextMessage)
        {
            await _messageSender.SendMessage(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value, publicName,
                emailTextMessage);
            return Ok();
        }
    }
}
