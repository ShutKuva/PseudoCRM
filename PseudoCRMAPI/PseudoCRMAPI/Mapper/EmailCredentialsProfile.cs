using AutoMapper;
using Core.Dtos.Email;
using Core.Email;

namespace PseudoCRMAPI.Mapper
{
    public class EmailCredentialsProfile : Profile
    {
        public EmailCredentialsProfile()
        {
            CreateMap<EmailCredentials, EmailDto>();
            CreateMap<EmailCredentialsDto, EmailCredentials>();
        }
    }
}
