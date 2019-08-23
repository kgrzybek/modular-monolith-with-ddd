using System.Security.Cryptography.X509Certificates;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Payers
{
    public interface IPayerContext
    {
        PayerId PayerId { get; }
    }
}