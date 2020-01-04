using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Domain.MeetingPayments
{
    internal class MeetingPaymentRepository : IMeetingPaymentRepository
    {
        private readonly PaymentsContext _paymentsContext;

        internal MeetingPaymentRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
        }

        public async Task AddAsync(MeetingPayment meetingPayment)
        {
            await _paymentsContext.MeetingPayments.AddAsync(meetingPayment);
        }

        public async Task<MeetingPayment> GetByIdAsync(PayerId payerId, MeetingId meetingId)
        {
            return await _paymentsContext.MeetingPayments.FirstOrDefaultAsync(x =>
                x.PayerId == payerId && x.MeetingId == meetingId);
        }
    }
}
