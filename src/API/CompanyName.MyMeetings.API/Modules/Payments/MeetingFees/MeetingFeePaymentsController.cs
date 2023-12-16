using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.CreateMeetingFeePayment;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeePaymentAsPaid;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Payments.MeetingFees
{
    [Route("api/payments/meetingFeePayments")]
    [ApiController]
    public class MeetingFeePaymentsController : ControllerBase
    {
        private readonly IPaymentsModule _meetingsModule;

        public MeetingFeePaymentsController(IPaymentsModule meetingsModule)
        {
            _meetingsModule = meetingsModule;
        }

        [HttpPost]
        [HasPermission(PaymentsPermissions.RegisterPayment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateMeetingFeePayment(CreateMeetingFeePaymentRequest request)
        {
            await _meetingsModule.ExecuteCommandAsync(new CreateMeetingFeePaymentCommand(request.MeetingFeeId));

            return Ok();
        }

        [HttpPut]
        [Route("{meetingFeePaymentId}/purchased")]
        [HasPermission(PaymentsPermissions.RegisterPayment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterMeetingFeePayment(
            Guid meetingFeePaymentId)
        {
            await _meetingsModule.ExecuteCommandAsync(new MarkMeetingFeePaymentAsPaidCommand(meetingFeePaymentId));

            return Ok();
        }
    }
}