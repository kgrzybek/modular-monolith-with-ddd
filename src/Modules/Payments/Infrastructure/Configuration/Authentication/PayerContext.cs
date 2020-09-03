using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Authentication
{
    internal class PayerContext : IPayerContext
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public PayerContext(IExecutionContextAccessor executionContextAccessor)
        {
            this._executionContextAccessor = executionContextAccessor;
        }

        public PayerId PayerId => new PayerId(_executionContextAccessor.UserId);
    }
}