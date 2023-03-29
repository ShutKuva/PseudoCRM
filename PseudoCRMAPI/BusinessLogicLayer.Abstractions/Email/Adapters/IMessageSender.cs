namespace BusinessLogicLayer.Abstractions.Email.Adapters
{
    public interface IMessageSender<U, E, T>
    {
        Task SendMessage(U user, E mailQuery, T message);
    }
}