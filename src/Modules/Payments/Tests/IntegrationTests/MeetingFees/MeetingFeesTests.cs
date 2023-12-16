using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.CreateMeetingFee;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.CreateMeetingFeePayment;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.GetMeetingFees;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeePaymentAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.MeetingFees
{
    [NonParallelizable]
    [TestFixture]
    public class MeetingFeesTests : TestBase
    {
        [Test]
        public async Task Create_Then_Pay_MeetingFee_Test()
        {
            var payerId = Guid.NewGuid();
            var meetingId = Guid.NewGuid();

            var meetingFeeId = await PaymentsModule.ExecuteCommandAsync(new CreateMeetingFeeCommand(
                Guid.NewGuid(),
                payerId,
                meetingId,
                100,
                "PLN"));

            var meetingFees = await GetEventually(new GetMeetingFeesProbe(PaymentsModule, meetingId, x => x != null && x.Count > 0), 5000);

            Assert.That(meetingFees[0].Status, Is.EqualTo(MeetingFeeStatus.WaitingForPayment.Code));

            var meetingFeePaymentId = await PaymentsModule.ExecuteCommandAsync(new CreateMeetingFeePaymentCommand(meetingFeeId));

            await PaymentsModule.ExecuteCommandAsync(new MarkMeetingFeePaymentAsPaidCommand(meetingFeePaymentId));

            meetingFees = await GetEventually(new GetMeetingFeesProbe(PaymentsModule, meetingId, x => x.Any(y => y.Status == MeetingFeePaymentStatus.Paid.Code)), 10000);

            Assert.That(meetingFees[0].Status, Is.EqualTo(MeetingFeeStatus.Paid.Code));
        }

        private class GetMeetingFeesProbe : IProbe<List<MeetingFeeDto>>
        {
            private readonly IPaymentsModule _paymentsModule;

            private readonly Guid _meetingId;

            private readonly Func<List<MeetingFeeDto>, bool> _condition;

            public GetMeetingFeesProbe(
                IPaymentsModule paymentsModule,
                Guid meetingId,
                Func<List<MeetingFeeDto>, bool> condition)
            {
                _paymentsModule = paymentsModule;
                _meetingId = meetingId;
                _condition = condition;
            }

            public bool IsSatisfied(List<MeetingFeeDto> sample)
            {
                return sample != null && _condition(sample);
            }

            public async Task<List<MeetingFeeDto>> GetSampleAsync()
            {
                return await _paymentsModule.ExecuteQueryAsync(new GetMeetingFeesQuery(_meetingId));
            }

            public string DescribeFailureTo()
            {
                return $"Cannot get meeting fees for MeetingId: {_meetingId}";
            }
        }
    }
}