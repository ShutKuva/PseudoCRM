using BusinessLogicLayer.Abstractions.Chat;
using DataAccessLayer.Abstractions;
using System.Linq.Expressions;
using Core.ChatEntities;

namespace BusinessLogicLayer.Chat
{
    public class MessageService : IMessageService<Message>
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IRepository<Message> messageRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Message message)
        {
            await _messageRepository.CreateAsync(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<Message> ReadAsync(Expression<Func<Message, bool>> predicate, int skip, int page)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> ReadCollectionAsync(Expression<Func<Message, bool>> predicate, int skip, int take, int page)
        {
            return _messageRepository.ReadCollectionAsync(predicate, skip, take, page);
        }
    }
}