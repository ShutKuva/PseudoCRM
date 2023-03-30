using AutoMapper;
using Core;
using Core.Dtos.User;

namespace PseudoCRMAPI.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRegistrationDto>();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserLoginDto>();
        }
    }
}
