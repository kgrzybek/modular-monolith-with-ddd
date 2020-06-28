using System.Data;
using System.Threading.Tasks;
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
                "([Id], [SubscriptionPeriodCode], [CountryCode], [MoneyValue], [MoneyCurrency], [IsActive])" +
                "VALUES (@PriceListItemId, @SubscriptionPeriodCode, @CountryCode, @Price, @Currency, @IsActive)",
                new
                {
                    @event.PriceListItemId,
                    @event.SubscriptionPeriodCode,
                    @event.CountryCode,
                    @event.Price,
                    @event.Currency,
                    @event.IsActive
                });
        }
    }
}