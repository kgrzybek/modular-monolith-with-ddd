using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.ActivatePriceListItem;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.ChangePriceListItemAttributes;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.CreatePriceListItem;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.DeactivatePriceListItem;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.GetPriceListItem;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Payments.PriceListItems
{
    [ApiController]
    [Route("api/payments/priceListItems")]
    public class PriceListItemsController : ControllerBase
    {
        private readonly IPaymentsModule _paymentsModule;

        public PriceListItemsController(IPaymentsModule paymentsModule)
        {
            _paymentsModule = paymentsModule;
        }

        [HttpGet]
        [HasPermission(PaymentsPermissions.GetPriceListItem)]
        [ProducesResponseType(typeof(PriceListItemMoneyValueDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPriceListItem([FromQuery] GetPriceListItemRequest request)
        {
            var priceListItem = await _paymentsModule.ExecuteQueryAsync(new GetPriceListItemQuery(
                request.CountryCode,
                request.CategoryCode,
                request.PeriodTypeCode));

            return Ok(priceListItem);
        }

        [HttpPost]
        [HasPermission(PaymentsPermissions.CreatePriceListItem)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePriceListItem([FromBody] CreatePriceListItemRequest request)
        {
            await _paymentsModule.ExecuteCommandAsync(new CreatePriceListItemCommand(
                request.SubscriptionPeriodCode,
                request.CategoryCode,
                request.CountryCode,
                request.PriceValue,
                request.PriceCurrency));

            return Ok();
        }

        [HttpPatch("{priceListItemId}/activate")]
        [HasPermission(PaymentsPermissions.ActivatePriceListItem)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ActivatePriceListItem([FromRoute] Guid priceListItemId)
        {
            await _paymentsModule.ExecuteCommandAsync(new ActivatePriceListItemCommand(priceListItemId));

            return Ok();
        }

        [HttpPatch("{priceListItemId}/deactivate")]
        [HasPermission(PaymentsPermissions.DeactivatePriceListItem)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeactivatePriceListItem([FromRoute] Guid priceListItemId)
        {
            await _paymentsModule.ExecuteCommandAsync(new DeactivatePriceListItemCommand(priceListItemId));

            return Ok();
        }

        [HttpPut]
        [HasPermission(PaymentsPermissions.ChangePriceListItemAttributes)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePriceListItemAttributes(
            [FromBody] ChangePriceListItemAttributesRequest request)
        {
            await _paymentsModule.ExecuteCommandAsync(new ChangePriceListItemAttributesCommand(
                request.PriceListItemId,
                request.CountryCode,
                request.SubscriptionPeriodCode,
                request.CategoryCode,
                request.PriceValue,
                request.PriceCurrency));

            return Ok();
        }
    }
}