using Core.BaseEntities;
using Core.Email;

namespace Core
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<EmailCredentials> Emails { get; set; } = new();
        public string PasswordHash { get; set; } = default!;
        public string? RefreshToken { get; set; }
    }
}