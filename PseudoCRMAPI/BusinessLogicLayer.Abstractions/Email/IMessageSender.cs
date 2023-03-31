namespace BusinessLogicLayer.Abstractions.Email
{
    public interface IMessageSender<U, E, T>
    {
        Task SendMessage(U user, E mailQuery, T message);
    }
}