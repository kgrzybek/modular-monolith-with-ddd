using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using Dapper;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription
{
    public class RenewSubscriptionCommandHandler : ICommandHandler<RenewSubscriptionCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public RenewSubscriptionCommandHandler(
            IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Unit> Handle(RenewSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscriptionRenewalPayment =
                await _aggregateStore.Load(new SubscriptionRenewalPaymentId(command.SubscriptionRenewalPaymentId));
            
            var subscription = await _aggregateStore.Load(new SubscriptionId(command.SubscriptionId));

            subscription.Renew(subscriptionRenewalPayment.GetSnapshot());

            _aggregateStore.AppendChanges(subscription);
            
            return Unit.Value;
        }
    }
}