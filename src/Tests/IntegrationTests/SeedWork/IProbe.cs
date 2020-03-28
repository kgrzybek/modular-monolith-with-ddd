using System.Threading.Tasks;

namespace CompanyName.MyMeetings.IntegrationTests.SeedWork
{
    public interface IProbe
    {
        bool IsSatisfied();

        Task SampleAsync();

        string DescribeFailureTo();
    }
}