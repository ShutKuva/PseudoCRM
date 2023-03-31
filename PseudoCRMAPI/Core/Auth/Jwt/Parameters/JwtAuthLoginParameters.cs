using Core.Auth.Jwt.Results;
using Core.Dtos.User;

namespace Core.Auth.Jwt.Parameters
{
    public class JwtAuthLoginParameters
    {
        public UserLoginDto? User { get; set; }
        public JwtResult? OldResult { get; set; }
    }
}