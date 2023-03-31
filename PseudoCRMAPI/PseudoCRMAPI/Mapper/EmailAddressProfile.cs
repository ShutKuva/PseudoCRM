using AutoMapper;
using MimeKit;

namespace PseudoCRMAPI.Mapper
{
    public class EmailAddressProfile : Profile
    {
        public EmailAddressProfile()
        {
            CreateMap<string, MailboxAddress>().ConvertUsing(s => new MailboxAddress("", s));
            CreateMap<MailboxAddress, string>().ConvertUsing(mail => mail.Address);
        }
    }
}
