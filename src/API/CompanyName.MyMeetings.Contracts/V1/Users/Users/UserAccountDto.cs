namespace CompanyName.MyMeetings.Contracts.V1.Users.Users;

public class UserAccountDto
{
    public Guid Id { get; init; }

    public string? Name { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    /// <summary>
    /// Gets or sets the date and time, in UTC, when any user lockout ends.
    /// A value in the past means the user is not locked out.
    /// </summary>
    public virtual DateTimeOffset? LockoutEnd { get; init; }

    /// <summary>
    /// Gets or sets a flag indicating if two factor authentication is enabled for this user.
    /// True if 2fa is enabled, otherwise false.
    /// </summary>
    public virtual bool TwoFactorEnabled { get; init; }

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their telephone address.
    /// True if the telephone number has been confirmed, otherwise false.
    /// </summary>
    public virtual bool PhoneNumberConfirmed { get; init; }

    /// <summary>
    /// Gets or sets a telephone number for the user.
    /// </summary>
    public virtual string? PhoneNumber { get; init; }

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their email address.
    /// True if the email address has been confirmed, otherwise false.
    /// </summary>
    public virtual bool EmailConfirmed { get; init; }

    /// <summary>
    /// Gets or sets the normalized email address for this user.
    /// </summary>
    public virtual string NormalizedEmail { get; init; } = null!;

    /// <summary>
    /// Gets or sets the email address for this user.
    /// </summary>
    public virtual string Email { get; init; } = null!;

    /// <summary>
    /// Gets or sets the normalized user name for this user.
    /// </summary>
    public virtual string NormalizedUserName { get; init; } = null!;

    /// <summary>
    /// Gets or sets the login for this user.
    /// </summary>
    public virtual string UserName { get; init; } = null!;

    /// <summary>
    /// True if the user could be locked out, otherwise false.
    /// </summary>
    public virtual bool LockoutEnabled { get; init; }

    /// <summary>
    /// Gets or sets the number of failed login attempts for the current user.
    /// </summary>
    public virtual int AccessFailedCount { get; init; }
}