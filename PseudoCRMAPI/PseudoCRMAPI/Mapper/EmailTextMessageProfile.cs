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
        private readonly IMapper _mapper;

        public EmailTextMessageToMimeMessageConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public MimeMessage Convert(EmailTextMessage source, MimeMessage destination, ResolutionContext context)
        {
            return new MimeMessage(_mapper.Map<IEnumerable<MailboxAddress>>(source.From), _mapper.Map<IEnumerable<MailboxAddress>>(source.To), source.Subject, new TextPart("plain", source.Text));
        }
    }
}
