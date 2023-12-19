namespace CompanyName.MyMeetings.BuildingBlocks.Application.Emails
{
    /// <summary>
    /// Represents an interface for sending emails.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email message.
        /// </summary>
        /// <param name="message">The email message to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendEmail(EmailMessage message);
    }
}