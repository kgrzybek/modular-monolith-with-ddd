using System.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.Events;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.GetPriceListItem
{
    internal class PriceListItemsProjector : ProjectorBase, IProjector
    {
        private readonly IDbConnection _connection;

        public PriceListItemsProjector(ISqlConnectionFactory sqlConnectionFactory)
        {
            _connection = sqlConnectionFactory.GetOpenConnection();
        }

        public async Task Project(IDomainEvent @event)
        {
            await When((dynamic)@event);
        }

        private async Task When(PriceListItemCreatedDomainEvent @event)
        {
            await _connection.ExecuteScalarAsync(
                "INSERT INTO payments.PriceListItems " +
                "([Id], [SubscriptionPeriodCode], [CategoryCode], [CountryCode], [MoneyValue], [MoneyCurrency], [IsActive])" +
                "VALUES (@PriceListItemId, @SubscriptionPeriodCode, @CategoryCode, @CountryCode, @Price, @Currency, @IsActive)",
                new
                {
                    @event.PriceListItemId,
                    @event.SubscriptionPeriodCode,
                    @event.CategoryCode,
                    @event.CountryCode,
                    @event.Price,
                    @event.Currency,
                    @event.IsActive
                });
        }

        private async Task When(PriceListItemActivatedDomainEvent @event)
        {
            await _connection.ExecuteScalarAsync(
                "UPDATE payments.PriceListItems " +
                "SET [IsActive] = 'true' " +
                "WHERE [Id] = @PriceListItemId",
                new
                {
                    @event.PriceListItemId
                });
        }

        private async Task When(PriceListItemDeactivatedDomainEvent @event)
        {
            await _connection.ExecuteScalarAsync(
                "UPDATE payments.PriceListItems " +
                "SET [IsActive] = 'false' " +
                "WHERE [Id] = @PriceListItemId",
                new
                {
                    @event.PriceListItemId
                });
        }

        private async Task When(PriceListItemAttributesChangedDomainEvent @event)
        {
            await _connection.ExecuteScalarAsync(
                "UPDATE payments.PriceListItems " +
                "SET " +
                    "[SubscriptionPeriodCode] = @SubscriptionPeriodCode," +
                    "[CountryCode] = @CountryCode," +
                    "[CategoryCode] = @CategoryCode," +
                    "[MoneyValue] = @Price," +
                    "[MoneyCurrency] = @Currency " +
                "WHERE [Id] = @PriceListItemId",
                new
                {
                    @event.PriceListItemId,
                    @event.SubscriptionPeriodCode,
                    @event.CountryCode,
                    @event.CategoryCode,
                    @event.Price,
                    @event.Currency
                });
        }
    }
}