namespace BusinessLogicLayer.Abstractions.Email.Adapters
{
    public interface IMessageSender<U, T>
    {
        void SendMessage(U user, T message);
    }
}