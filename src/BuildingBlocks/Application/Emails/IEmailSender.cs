using System.Threading.Tasks;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Emails
{
    public interface IEmailSender
    {
        Task SendEmail(EmailMessage message);
    }
}