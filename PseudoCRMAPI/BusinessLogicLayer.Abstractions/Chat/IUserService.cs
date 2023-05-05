using System.Linq.Expressions;
using BusinessLogicLayer.Abstractions.Services;

namespace BusinessLogicLayer.Abstractions.Chat
{
    public interface IUserService<TUser> : IReadService<TUser>
    {
    }
}