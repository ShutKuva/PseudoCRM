using Core.BaseEntities;
using Core.Email;

namespace Core
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; }
        public List<EmailCredentials> Emails { get; set; } = new();
        public string PasswordHash { get; set; } = default!;
        public string? RefreshToken { get; set; }
    }
}