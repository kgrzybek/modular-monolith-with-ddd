using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.CreatePriceListItem;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Payments.PriceListItems
{
    [ApiController]
    [Route("payments/priceListItems")]
    public class PriceListItemsController : ControllerBase
    {
        private readonly IPaymentsModule _paymentsModule;

        public PriceListItemsController(IPaymentsModule paymentsModule)
        {
            _paymentsModule = paymentsModule;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePriceListItemRequest request)
        {
            await _paymentsModule.ExecuteCommandAsync(new CreatePriceListItemCommand(
                Guid.NewGuid(),
                request.SubscriptionPeriodCode,
                request.CategoryCode,
                request.CountryCode,
                request.PriceValue,
                request.PriceCurrency));

            return Ok();
        }
    }
}