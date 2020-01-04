using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.CreatePayer
{
    internal class CreatePayerCommand : InternalCommandBase
    {
        internal Guid UserId {get; }
        internal string Login { get; }

        internal string Email { get; }

        internal string FirstName { get; }

        internal string LastName { get; }

        internal string Name { get; }

        [JsonConstructor]
        internal CreatePayerCommand(Guid id, Guid userId, string login, string email, string firstName, string lastName, string name) : base(id)
        {
            UserId = userId;
            Login = login;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
        }
    }
}