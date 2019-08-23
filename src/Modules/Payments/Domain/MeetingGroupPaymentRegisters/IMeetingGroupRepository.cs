using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters
{
    public interface IMeetingGroupPaymentRegisterRepository
    {
        Task AddAsync(MeetingGroupPaymentRegister paymentRegister);

        Task<MeetingGroupPaymentRegister> GetByIdAsync(MeetingGroupPaymentRegisterId paymentRegisterId);
    }
}