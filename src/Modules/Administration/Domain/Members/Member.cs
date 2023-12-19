using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Administration.Domain.Members.Events;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.Members
{
    /// <summary>
    /// Represents a member in the system.
    /// </summary>
    public class Member : Entity, IAggregateRoot
    {
        public MemberId Id { get; private set; }

        private string _login;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _name;
        private DateTime _createDate;

        private Member()
        {
            // Only for EF.
        }

        private Member(Guid id, string login, string email, string firstName, string lastName, string name)
        {
            this.Id = new MemberId(id);
            _login = login;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = name;
            _createDate = DateTime.UtcNow;

            this.AddDomainEvent(new MemberCreatedDomainEvent(this.Id));
        }

        /// <summary>
        /// Creates a new member.
        /// </summary>
        /// <param name="id">The unique identifier of the member.</param>
        /// <param name="login">The login of the member.</param>
        /// <param name="email">The email of the member.</param>
        /// <param name="firstName">The first name of the member.</param>
        /// <param name="lastName">The last name of the member.</param>
        /// <param name="name">The full name of the member.</param>
        /// <returns>The created member.</returns>
        public static Member Create(Guid id, string login, string email, string firstName, string lastName, string name)
        {
            return new Member(id, login, email, firstName, lastName, name);
        }
    }
}