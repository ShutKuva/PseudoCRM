namespace BusinessLogicLayer.Abstractions.Email.Adapters
{
    public interface IStringMessageSenderAdapter<T> : IMessageSender<string, string, T>
    {
    }
}
