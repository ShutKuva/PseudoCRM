namespace Core.Dtos.User
{
    public class UserRegistrationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}