using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.CreatePriceListItem;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.GetPriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.PriceList
{
    public static class PriceListHelper
    {
        public static async Task AddPriceListItems(IPaymentsModule paymentsModule)
        {
            await paymentsModule.ExecuteCommandAsync(new CreatePriceListItemCommand(
                SubscriptionPeriod.Month.Code,
                PriceListItemCategory.New.Code,
                "PL",
                60,
                "PLN"));

            await paymentsModule.ExecuteCommandAsync(new CreatePriceListItemCommand(
                SubscriptionPeriod.HalfYear.Code,
                PriceListItemCategory.New.Code,
                "PL",
                320,
                "PLN"));

            await paymentsModule.ExecuteCommandAsync(new CreatePriceListItemCommand(
                SubscriptionPeriod.Month.Code,
                PriceListItemCategory.New.Code,
                "US",
                15,
                "USD"));

            await paymentsModule.ExecuteCommandAsync(new CreatePriceListItemCommand(
                SubscriptionPeriod.HalfYear.Code,
                PriceListItemCategory.New.Code,
                "US",
                80,
                "USD"));

            await paymentsModule.ExecuteCommandAsync(new CreatePriceListItemCommand(
                SubscriptionPeriod.HalfYear.Code,
                PriceListItemCategory.Renewal.Code,
                "PL",
                320,
                "PLN"));

            await TestBase.GetEventually(new GetPriceListProbe(paymentsModule, x => x.Count == 5), 5000);
        }

        private class GetPriceListProbe : IProbe<List<PriceListItemDto>>
        {
            private readonly IPaymentsModule _paymentsModule;

            private readonly Func<List<PriceListItemDto>, bool> _condition;

            public GetPriceListProbe(
                IPaymentsModule paymentsModule,
                Func<List<PriceListItemDto>, bool> condition)
            {
                _paymentsModule = paymentsModule;
                _condition = condition;
            }

            public bool IsSatisfied(List<PriceListItemDto> sample)
            {
                return sample != null && _condition(sample);
            }

            public async Task<List<PriceListItemDto>> GetSampleAsync()
            {
                return await _paymentsModule.ExecuteQueryAsync(new GetPriceListItemsQuery());
            }

            public string DescribeFailureTo()
            {
                return "Cannot get price list for specified condition";
            }
        }
    }
}