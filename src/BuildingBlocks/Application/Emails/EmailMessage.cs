namespace CompanyName.MyMeetings.BuildingBlocks.Application.Emails
{
    /// <summary>
    /// Represents an email message.
    /// </summary>
    public struct EmailMessage
    {
        /// <summary>
        /// Gets the recipient of the email.
        /// </summary>
        public string To { get; }

        /// <summary>
        /// Gets the subject of the email.
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// Gets the content of the email.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> struct.
        /// </summary>
        /// <param name="to">The recipient of the email.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="content">The content of the email.</param>
        public EmailMessage(
            string to,
            string subject,
            string content)
        {
            this.To = to;
            this.Subject = subject;
            this.Content = content;
        }
    }
}