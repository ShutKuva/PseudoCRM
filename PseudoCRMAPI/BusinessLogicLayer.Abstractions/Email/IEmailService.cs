using Core.Email;
using Core.Email.Additional;
using MailKit.Search;

namespace BusinessLogicLayer.Abstractions.Email
{
    public interface IEmailService<U, M>
    {
        Task<IReadOnlyList<M>> GetMessages(U user, string publicName, ServerProtocols serverProtocol = ServerProtocols.Imap);

        Task<IReadOnlyList<M>> GetMessages(U user, string publicName, int takeLast);

        Task<IReadOnlyList<M>> GetMessages(U user, string publicName, SearchQuery searchQuery);

        Task SendMessage(U user, string publicName, M message);

        Task SetNewEmail(U user, EmailCredentials emailCredentials);
    }
}