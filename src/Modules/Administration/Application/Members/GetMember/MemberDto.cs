namespace CompanyName.MyMeetings.Modules.Administration.Application.Members.GetMember
{
    /// <summary>
    /// Represents a data transfer object for a member.
    /// </summary>
    public class MemberDto
    {
        /// <summary>
        /// Gets or sets the member unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the member login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the member email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the member first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the member last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the member full name.
        /// </summary>
        public string Name { get; set; }
    }
}