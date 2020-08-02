using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Users
{
    internal class UserContext : IUserContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public UserContext(IExecutionContextAccessor executionContextAccessor)
        {
            this._executionContextAccessor = executionContextAccessor;
        }

        public UserId UserId => new UserId(_executionContextAccessor.UserId);
    }
}