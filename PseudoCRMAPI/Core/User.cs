using Core.BaseEntities;
using Core.Email;

namespace Core
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; }
        public int? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public List<EmailCredentials> Emails { get; set; } = new();
        public string PasswordHash { get; set; } = default!;
        public string? RefreshToken { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not User userObj)
            {
                return false;
            }

            return Name == userObj.Name && Email == userObj.Email && PasswordHash == userObj.PasswordHash;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Emails, PasswordHash);
        }
    }
}