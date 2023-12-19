using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users.Events;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;

public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    private List<IDomainEvent>? _domainEvents;

    public ApplicationUser(string userName)
    : base(userName)
    {
        AddDomainEvent(new UserCreatedDomainEvent(Id));
    }

    private ApplicationUser()
        : base()
    {
        // Only EF.
    }

    internal static ApplicationUser CreateFromUserRegistration(
            UserRegistrationId userRegistrationId,
            string login,
            string email,
            string firstName,
            string lastName,
            string name)
    {
        return new ApplicationUser(login)
        {
            Id = userRegistrationId.Value,
            FirstName = firstName,
            LastName = lastName,
            Name = name,
            Email = email
        };
    }

    /// <summary>
    /// Domain events occurred.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    /// <summary>
    /// Add domain event.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();

        _domainEvents.Add(domainEvent);
    }

    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    public virtual string? Name { get; set; }

    public virtual string? FirstName { get; set; }

    public virtual string? LastName { get; set; }
}