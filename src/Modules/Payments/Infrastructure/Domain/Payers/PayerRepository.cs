using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Domain.Payers
{
    public class PayerRepository : IPayerRepository
    {
        private readonly PaymentsContext _paymentsContext;

        public PayerRepository(PaymentsContext paymentsContext)
        {
            _paymentsContext = paymentsContext;
        }

        public async Task AddAsync(Payer payer)
        {
            await _paymentsContext.Payers.AddAsync(payer);
        }
    }
}