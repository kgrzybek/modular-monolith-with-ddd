﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.DeactivatePriceListItem
{
    internal class DeactivatePriceListItemCommandHandler : ICommandHandler<DeactivatePriceListItemCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public DeactivatePriceListItemCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Unit> Handle(DeactivatePriceListItemCommand command, CancellationToken cancellationToken)
        {
            var priceListItem = await _aggregateStore.Load(new PriceListItemId(command.PriceListItemId));

            if (priceListItem == null)
            {
                throw new InvalidCommandException(new List<string> { "Pricelist item for deactivation must exist." });
            }

            priceListItem.Deactivate();

            _aggregateStore.AppendChanges(priceListItem);

            return Unit.Value;
        }
    }
}