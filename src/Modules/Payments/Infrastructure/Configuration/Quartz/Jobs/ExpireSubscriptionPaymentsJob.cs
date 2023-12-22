using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing;
using Quartz;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Quartz.Jobs
{
    public class ExpireSubscriptionPaymentsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ExpireSubscriptionPaymentsCommand());
        }
    }
}