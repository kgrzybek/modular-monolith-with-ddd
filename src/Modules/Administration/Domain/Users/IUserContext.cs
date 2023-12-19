namespace CompanyName.MyMeetings.Modules.Administration.Domain.Users
{
    /// <summary>
    /// Represents the user context interface.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Gets the user ID.
        /// </summary>
        UserId UserId { get; }
    }
}