using System.Linq.Expressions;
using BusinessLogicLayer.Abstractions.Services;

namespace BusinessLogicLayer.Abstractions.Chat
{
    public interface IMessageService<TMessage> : ICreateService<TMessage>, IReadService<TMessage>
    {
    }
}