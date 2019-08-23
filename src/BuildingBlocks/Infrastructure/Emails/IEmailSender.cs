using System.Threading.Tasks;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessage message);
    }
}