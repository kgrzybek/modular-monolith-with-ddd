using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments
{
    public interface IMeetingPaymentRepository
    {
        Task AddAsync(MeetingPayment meetingPayment);

        Task<MeetingPayment> GetByIdAsync(PayerId payerId, MeetingId meetingId);
    }
}