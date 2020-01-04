using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember
{
    internal class CreateMemberCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal CreateMemberCommand(Guid id,
            Guid memberId,
            string login,
            string email,
            string firstName,
            string lastName,
            string name) : base(id)
        {
            Login = login;
            MemberId = memberId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
        }
        internal Guid MemberId { get; }
        internal string Login { get; }

        internal string Email { get; }

        internal string FirstName { get; }

        internal string LastName { get; }

        internal string Name { get; }
    }
}