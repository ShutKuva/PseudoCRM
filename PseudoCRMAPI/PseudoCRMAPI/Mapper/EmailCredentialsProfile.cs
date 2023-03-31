using AutoMapper;
using Core.Dtos;
using Core.Email;

namespace PseudoCRMAPI.Mapper
{
    public class EmailCredentialsProfile : Profile
    {
        public EmailCredentialsProfile()
        {
            CreateMap<EmailCredentials, EmailDto>();
        }
    }
}
