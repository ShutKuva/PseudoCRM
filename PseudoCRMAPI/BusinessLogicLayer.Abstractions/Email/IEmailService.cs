using Core.Email;
using Core;
using Core.Dtos;

namespace BusinessLogicLayer.Abstractions.Email
{
    public interface IEmailService<U, E>
    {
        Task SetNewEmail(U user, E emailCredentials);
        Task<IEnumerable<EmailDto>> GetRegisteredPublicNames(U user);
    }
}