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
            CreateMap<UserRegistrationDto, User>().ForMember(urd => urd.Name, config => config.MapFrom((urd, u) => urd.Login));
            CreateMap<User, UserLoginDto>();
        }
    }
}
