using Core.BaseEntities;
using Core.Email;

namespace Core
{
    public class User : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<EmailCredentials> Emails { get; set; }
        public string PasswordHash { get; set; }
    }
}