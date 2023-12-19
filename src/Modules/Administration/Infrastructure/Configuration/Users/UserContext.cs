using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Users
{
    /// <summary>
    /// Represents the user context implementation that provides access to the current user's information.
    /// </summary>
    internal class UserContext : IUserContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class.
        /// </summary>
        /// <param name="executionContextAccessor">The execution context accessor.</param>
        public UserContext(IExecutionContextAccessor executionContextAccessor)
        {
            this._executionContextAccessor = executionContextAccessor;
        }

        /// <summary>
        /// Gets the user ID of the current user.
        /// </summary>
        public UserId UserId => new UserId(_executionContextAccessor.UserId);
    }
}