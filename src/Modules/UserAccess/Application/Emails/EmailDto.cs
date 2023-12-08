namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Emails
{
    public class EmailDto
    {
        public Guid Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
}