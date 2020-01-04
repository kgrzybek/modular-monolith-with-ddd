using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Domain.MeetingGroupPaymentRegisters
{
    internal class MeetingGroupPaymentRegisterRepository : IMeetingGroupPaymentRegisterRepository
    {
        private readonly PaymentsContext _paymentsContext;

        internal MeetingGroupPaymentRegisterRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
        }

        public async Task AddAsync(MeetingGroupPaymentRegister paymentRegister)
        {
            await _paymentsContext.MeetingGroupPaymentRegisters.AddAsync(paymentRegister);
        }

        public async Task<MeetingGroupPaymentRegister> GetByIdAsync(MeetingGroupPaymentRegisterId paymentRegisterId)
        {
            return await _paymentsContext.MeetingGroupPaymentRegisters.FirstOrDefaultAsync(x =>
                x.Id == paymentRegisterId);
        }
    }
}
