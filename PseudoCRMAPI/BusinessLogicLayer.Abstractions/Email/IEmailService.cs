using Core.Dtos.Email;

namespace BusinessLogicLayer.Abstractions.Email
{
    public interface IEmailService<U, E, S>
    {
        Task SetNewEmail(U user, E emailCredentials);
        Task SetNewServerInfo(U user, E emailCredentials, S serverInfo);
        Task<IEnumerable<EmailDto>> GetRegisteredPublicNames(U user);
        Task<bool> CheckServerInfoAvailability(U user, E emailCredentials, S serverInfo);
    }
}