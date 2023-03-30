namespace BusinessLogicLayer.Abstractions.Email.Adapters
{
    public interface IStringMessageReceiverAdapter<T, S> : IMessageReceiver<T, string, string, S>
    {
    }
}