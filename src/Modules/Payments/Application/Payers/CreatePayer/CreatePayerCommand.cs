using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.CreatePayer
{
    public class CreatePayerCommand : InternalCommandBase<Guid>
    {
        internal Guid UserId { get; }

        internal string Login { get; }

        internal string Email { get; }

        internal string FirstName { get; }

        internal string LastName { get; }

        internal string Name { get; }

        [JsonConstructor]
        public CreatePayerCommand(
            Guid id,
            Guid userId,
            string login,
            string email,
            string firstName,
            string lastName,
            string name)
            : base(id)
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