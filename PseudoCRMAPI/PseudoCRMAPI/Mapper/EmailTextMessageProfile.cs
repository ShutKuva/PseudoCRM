using AutoMapper;
using Core.Email;
using MimeKit;

namespace PseudoCRMAPI.Mapper
{
    public class EmailTextMessageProfile : Profile
    {
        public EmailTextMessageProfile()
        {
            CreateMap<EmailTextMessage, MimeMessage>().ConvertUsing<EmailTextMessageToMimeMessageConverter>();
            CreateMap<MimeMessage, EmailTextMessage>().ForMember(e => e.Text, conf => conf.MapFrom(m => m.TextBody));
        }
    }

    public class EmailTextMessageToMimeMessageConverter : ITypeConverter<EmailTextMessage, MimeMessage>
    {
        public MimeMessage Convert(EmailTextMessage source, MimeMessage destination, ResolutionContext context)
        {
            return new MimeMessage(source.From, source.To, source.Subject, new TextPart("plain", source.Text));
        }
    }
}
