using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions
{
    public class RenewSubscriptionCommandHandler : ICommandHandler<RenewSubscriptionCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public RenewSubscriptionCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Unit> Handle(RenewSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _aggregateStore.Load(new SubscriptionId(command.SubscriptionId));

            return Unit.Value;
        }
    }
}