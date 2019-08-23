using System.Threading.Tasks;
using Serilog;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly EmailsConfiguration _configuration;
        public EmailSender(ILogger logger, EmailsConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void SendEmail(EmailMessage message)
        {
            _logger.Information(
                "Email sent. From: {From}, To: {To}, Subject: {Subject}, Content: {Content}.", 
                _configuration.FromEmail, 
                message.To, 
                message.Subject,
                message.Content);
        }
    }
}