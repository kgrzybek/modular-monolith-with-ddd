using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Events;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Rules;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations
{
    public class UserRegistration : Entity, IAggregateRoot
    {
        public UserRegistrationId Id { get; private set; }

        private string _login;

        private string _password;

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _name;

        private DateTime _registerDate;

        private UserRegistrationStatus _status;

        private DateTime? _confirmedDate;

        private UserRegistration()
        {
            // Only EF.
        }

        public static UserRegistration RegisterNewUser(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            IUsersCounter usersCounter,
            string confirmLink)
        {
            return new UserRegistration(login, password, email, firstName, lastName, usersCounter, confirmLink);
        }

        private UserRegistration(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            IUsersCounter usersCounter,
            string confirmLink)
        {
            this.CheckRule(new UserLoginMustBeUniqueRule(usersCounter, login));

            this.Id = new UserRegistrationId(Guid.NewGuid());
            _login = login;
            _password = password;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = $"{firstName} {lastName}";
            _registerDate = DateTime.UtcNow;
            _status = UserRegistrationStatus.WaitingForConfirmation;

            this.AddDomainEvent(new NewUserRegisteredDomainEvent(
                this.Id,
                _login,
                _email,
                _firstName,
                _lastName,
                _name,
                _registerDate,
                confirmLink));
        }

        public User CreateUser()
        {
            this.CheckRule(new UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(_status));

            return User.CreateFromUserRegistration(
                this.Id,
                this._login,
                this._password,
                this._email,
                this._firstName,
                this._lastName,
                this._name);
        }

        public void Confirm()
        {
            this.CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
            this.CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(_status));

            _status = UserRegistrationStatus.Confirmed;
            _confirmedDate = DateTime.UtcNow;

            this.AddDomainEvent(new UserRegistrationConfirmedDomainEvent(this.Id));
        }

        public void Expire()
        {
            this.CheckRule(new UserRegistrationCannotBeExpiredMoreThanOnceRule(_status));

            _status = UserRegistrationStatus.Expired;

            this.AddDomainEvent(new UserRegistrationExpiredDomainEvent(this.Id));
        }
    }
}