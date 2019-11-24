using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.RegisterPayment
{
    internal class RegisterPaymentCommandHandler : ICommandHandler<RegisterPaymentCommand>
    {
        private readonly IMeetingGroupPaymentRegisterRepository _meetingGroupPaymentRegisterRepository;
        private readonly IPayerContext _payerContext;

        public RegisterPaymentCommandHandler(IMeetingGroupPaymentRegisterRepository meetingGroupPaymentRegisterRepository, IPayerContext payerContext)
        {
            _meetingGroupPaymentRegisterRepository = meetingGroupPaymentRegisterRepository;
            _payerContext = payerContext;
        }

        public async Task<Unit> Handle(RegisterPaymentCommand command, CancellationToken cancellationToken)
        {
            var paymentRegister =
                await _meetingGroupPaymentRegisterRepository.GetByIdAsync(
                    new MeetingGroupPaymentRegisterId(command.MeetingGroupId));

            paymentRegister.RegisterPayment(PaymentTerm.Create(command.StartDate, command.EndDate), _payerContext.PayerId);

            return Unit.Value;
        }
    }
}