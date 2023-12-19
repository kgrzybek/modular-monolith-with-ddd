using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations.Events;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations.Rules;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;

public class UserRegistration : Entity, IAggregateRoot
{
    public UserRegistrationId Id { get; private set; } = null!;

    private string _login = null!;

    private string _password = null!;

    private string _email = null!;

    private string _firstName = null!;

    private string _lastName = null!;

    private string _name = null!;

    private DateTime _registerDate;

    private UserRegistrationStatus _status = null!;

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
        CheckRule(new UserLoginMustBeUniqueRule(usersCounter, login));

        Id = new UserRegistrationId(Guid.NewGuid());
        _login = login;
        _password = password;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _name = $"{firstName} {lastName}";
        _registerDate = DateTime.UtcNow;
        _status = UserRegistrationStatus.WaitingForConfirmation;

        AddDomainEvent(new NewUserRegisteredDomainEvent(
            Id,
            _login,
            _email,
            _firstName,
            _lastName,
            _name,
            _registerDate,
            confirmLink));
    }

    public string Password => _password;

    public ApplicationUser CreateUser()
    {
        CheckRule(new UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(_status));

        return ApplicationUser.CreateFromUserRegistration(
            Id,
            _login,
            _email,
            _firstName,
            _lastName,
            _name);
    }

    public void Confirm()
    {
        CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
        CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(_status));

        _status = UserRegistrationStatus.Confirmed;
        _confirmedDate = DateTime.UtcNow;

        AddDomainEvent(new UserRegistrationConfirmedDomainEvent(Id));
    }

    public void Expire()
    {
        CheckRule(new UserRegistrationCannotBeExpiredMoreThanOnceRule(_status));

        _status = UserRegistrationStatus.Expired;

        AddDomainEvent(new UserRegistrationExpiredDomainEvent(Id));
    }
}