using BusinessLogicLayer.Abstractions.Email;
using Core;
using Core.Auth.Jwt.Parameters;
using Core.Email;
using Core.Email.Additional;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using Core.Dtos.Email;
using BusinessLogicLayer.Abstractions.Email.Adapters;

namespace PseudoCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService<string, EmailCredentialsDto, ServerInformation> _emailService;
        private readonly IMessageReceiver<IReadOnlyList<EmailTextMessage>, string, string, ServerProtocols> _sharedMessageReceiver;
        private readonly IMessageReceiver<IReadOnlyList<EmailTextMessage>, string, string, SearchQuery> _imapMessageReceiver;
        private readonly IMessageReceiver<IReadOnlyList<EmailTextMessage>, string, string, int> _popMessageReceiver;
        private readonly IMessageSender<string, string, EmailTextMessage> _messageSender;

        public EmailController(
            IEmailService<string, EmailCredentialsDto, ServerInformation> emailService,
            IStringMessageReceiverAdapter<IReadOnlyList<EmailTextMessage>, ServerProtocols> sharedMessageReceiver,
            IStringMessageReceiverAdapter<IReadOnlyList<EmailTextMessage>, SearchQuery> imapMessageReceiver,
            IStringMessageReceiverAdapter<IReadOnlyList<EmailTextMessage>, int> popMessageReceiver,
            IStringMessageSenderAdapter<EmailTextMessage> messageSender)
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
        public async Task<OkResult> SetNewEmailCredentials([FromBody] EmailCredentialsDto emailCredentials)
        {
            await _emailService.SetNewEmail(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value,
                emailCredentials);
            return Ok();
        }

        [HttpPost("{publicName}/set-server-info")]
        public async Task<OkResult> SetNewServerInfo(string publicName, [FromBody] ServerInformation serverInformation)
        {
            await _emailService.SetNewServerInfo(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value, new EmailCredentialsDto(){PublicName = publicName},
                serverInformation);
            return Ok();
        }

        [HttpGet("{publicName}/pop/messages/{getLast}")]
        public async Task<ActionResult<IReadOnlyList<EmailTextMessage>>> GetMessageWithPop(string publicName, int getLast)
        {
            return Ok(await _popMessageReceiver.GetMessages(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value, publicName, getLast));
        }

        [HttpGet("{publicName}/imap/messages")]
        public async Task<ActionResult<IReadOnlyList<EmailTextMessage>>> GetMessageWithImap(string publicName)
        {
            return Ok(await _sharedMessageReceiver.GetMessages(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value, publicName, ServerProtocols.Imap));
        }

        [HttpPost("{publicName}/smtp/send")]
        public async Task<OkResult> SendMessage(string publicName, [FromBody]EmailTextMessage emailTextMessage)
        {
            await _messageSender.SendMessage(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value, publicName,
                emailTextMessage);
            return Ok();
        }

        [HttpGet("{publicName}/{protocol}/check")]
        public async Task<OkObjectResult> CheckAvailability(string publicName, string protocol)
        {
            return Ok(await _emailService.CheckServerInfoAvailability(User.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value,
                new EmailCredentialsDto(){PublicName = publicName},
                new ServerInformation(){ServerProtocol = protocol switch
                {
                    "imap" => ServerProtocols.Imap,
                    "smtp" => ServerProtocols.Smtp,
                    "pop" => ServerProtocols.Pop
                }}));
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test");
        }
    }
}
