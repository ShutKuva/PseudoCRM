namespace Core.Dtos.User
{
    public class UserRegistrationDto
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? OrganizationName { get; set; }
    }
}