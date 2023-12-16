namespace CompanyName.MyMeetings.Modules.Payments.Domain.Payers
{
    public interface IPayerRepository
    {
        Task AddAsync(Payer payer);
    }
}