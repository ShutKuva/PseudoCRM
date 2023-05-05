using BusinessLogicLayer.Abstractions.Chat;
using Core;
using Core.Auth.Jwt.Parameters;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using BusinessLogicLayer.Abstractions.Chat.Facades;
using Core.ChatEntities;

namespace PseudoCRMAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService<User> _userService;
        private readonly IMessageFacade _messageFacade;
        private readonly ConcurrentDictionary<User, List<string>> _userDictionary;

        public ChatHub(IUserService<User> userService, IMessageFacade messageFacade)
        {
            _userService = userService;
            _messageFacade = messageFacade;
            _userDictionary = new ConcurrentDictionary<User, List<string>>();
        }

        public override async Task OnConnectedAsync()
        {
            User user = await _userService.ReadAsync(user => user.Id == int.Parse(Context.User.FindFirst(claim => claim.Type == ClaimNames.Id).Value), 0, 0);
            
            if (_userDictionary.ContainsKey(user))
            {
                lock (_userDictionary[user])
                {
                    _userDictionary[user].Add(Context.ConnectionId);
                }
            }
            else
            {
                _userDictionary[user] = new List<string>() { Context.ConnectionId };
            }

            if (user.OrganizationId != null)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, user.OrganizationId.ToString());
            }

            await Clients.Group(user.Id.ToString()).SendAsync("IsOnline", user.Id);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            int parsedId = int.Parse(Context.User?.FindFirst(claim => claim.Type == ClaimNames.Id).Value);

            User? user = await _userService.ReadAsync(user => user.Id == parsedId, 0, 0);

            if (user == null)
            {
                throw new ArgumentException("There is no user with this id.");
            }

            Task sendTask = Task.CompletedTask;

            lock (_userDictionary[user])
            {
                _userDictionary[user].Remove(Context.ConnectionId);

                if (_userDictionary[user].Count == 0)
                {
                    sendTask = Clients.Group(user.Id.ToString()).SendAsync("IsOffline", user.Id);
                }
            }

            await sendTask;
        }

        public async Task IsOnline(int id)
        {
            User? user = await _userService.ReadAsync(user => user.Id == id, 0, 0);

            if (user == null)
            {
                throw new ArgumentException("There is no user with this id.");
            }

            Task sendTask = Task.CompletedTask;

            lock (_userDictionary[user])
            {
                sendTask = Clients.Caller.SendAsync("IsOnlineResponse", _userDictionary[user].Count != 0);
            }

            await sendTask;

            await Groups.AddToGroupAsync(Context.ConnectionId, user.Id.ToString());
        }

        public async Task SendMessage(string messageText)
        {
            int parsedId = int.Parse(Context.User?.FindFirst(claim => claim.Type == ClaimNames.Id).Value);

            User user = await _userService.ReadAsync(user => user.Id == parsedId, 0, 0);

            await Clients.Group(user.OrganizationId.ToString()).SendAsync("NewMessage", await _messageFacade.AddMessageByUserAsync(user, messageText));
        }
    }
}
