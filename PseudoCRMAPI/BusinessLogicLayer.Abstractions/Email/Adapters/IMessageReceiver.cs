namespace BusinessLogicLayer.Abstractions.Email.Adapters
{
    public interface IMessageReceiver<T, U, E, S>
    {
        public Task<T> GetMessages(U user, E mailQuery, S messageSearchQuery);
    }
}