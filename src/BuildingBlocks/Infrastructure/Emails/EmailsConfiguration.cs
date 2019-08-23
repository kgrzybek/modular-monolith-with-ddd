namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails
{
    public class EmailsConfiguration
    {
        public EmailsConfiguration(string fromEmail)
        {
            FromEmail = fromEmail;
        }

        public string FromEmail { get; }
    }
}