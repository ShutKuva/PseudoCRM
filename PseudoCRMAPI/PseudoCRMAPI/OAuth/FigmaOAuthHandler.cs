using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace PseudoCRMAPI.OAuth
{
    public class FigmaOAuthHandler : OAuthHandler<FigmaOAuthHandlerOptions>
    {
        public FigmaOAuthHandler(IOptionsMonitor<FigmaOAuthHandlerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }
    }
}
