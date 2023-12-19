namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails
{
    /// <summary>
    /// Represents the configuration for emails.
    /// </summary>
    public class EmailsConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailsConfiguration"/> class.
        /// </summary>
        /// <param name="fromEmail">The email address from which the emails will be sent.</param>
        public EmailsConfiguration(string fromEmail)
        {
            FromEmail = fromEmail;
        }

        /// <summary>
        /// Gets the email address from which the emails will be sent.
        /// </summary>
        public string FromEmail { get; }
    }
}