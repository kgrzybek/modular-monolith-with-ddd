using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Members.CreateMember
{
    /// <summary>
    /// Represents a command to create a member.
    /// </summary>
    public class CreateMemberCommand : InternalCommandBase<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMemberCommand"/> class.
        /// </summary>
        /// <param name="id">The command ID .</param>
        /// <param name="memberId">Gets the member ID.</param>
        /// <param name="login">The login.</param>
        /// <param name="email">The email.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="name">The name.</param>
        [JsonConstructor]
        public CreateMemberCommand(
            Guid id,
            Guid memberId,
            string login,
            string email,
            string firstName,
            string lastName,
            string name)
            : base(id)
        {
            Login = login;
            MemberId = memberId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
        }

        /// <summary>
        /// Gets the member ID.
        /// </summary>
        internal Guid MemberId { get; }

        /// <summary>
        /// Gets the login.
        /// </summary>
        internal string Login { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        internal string Email { get; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        internal string FirstName { get; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        internal string LastName { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        internal string Name { get; }
    }
}