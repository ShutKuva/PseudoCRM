using Core.Dtos.User;

namespace Core.Auth.Jwt.Parameters
{
    public class JwtAuthRegistrationParameters
    {
        public UserRegistrationDto User { get; set; }
    }
}