using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Abstractions.Chat.Adapters;
using Core;
using Core.ChatEntities;

namespace BusinessLogicLayer.Chat.Adapters
{
    public class MessageServiceAdapter : IMessageServiceAdapter<Message>
    {
        private readonly IOrganizationService<Organization> _organizationService;

        public MessageServiceAdapter(IOrganizationService<Organization> organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string organizationName)
        {
            if (string.IsNullOrEmpty(organizationName))
            {
                throw new ArgumentException("User is no in organization.");
            }

            Organization? organization =
                await _organizationService.ReadAsync(organization => organization.Name == organizationName, 0, 0);

            if (organization == null)
            {
                throw new ArgumentException("There is no organization with this name.");
            }

            return organization.Chat.Messages;
        }
    }
}