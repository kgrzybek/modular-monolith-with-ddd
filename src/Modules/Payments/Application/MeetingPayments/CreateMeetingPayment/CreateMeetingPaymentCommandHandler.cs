using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingPayments.CreateMeetingPayment
{
    internal class CreateMeetingPaymentCommandHandler : ICommandHandler<CreateMeetingPaymentCommand>
    {
        private readonly IMeetingPaymentRepository _meetingPaymentRepository;

        public CreateMeetingPaymentCommandHandler(IMeetingPaymentRepository meetingPaymentRepository)
        {
            _meetingPaymentRepository = meetingPaymentRepository;
        }

        public async Task<Unit> Handle(CreateMeetingPaymentCommand request, CancellationToken cancellationToken)
        {
            var meetingPayment = MeetingPayment.CreatePaymentForMeeting(
                request.PayerId, 
                request.MeetingId,
                MoneyValue.Of(request.Value, request.Currency));

            await _meetingPaymentRepository.AddAsync(meetingPayment);

            return Unit.Value;
        }
    }
}