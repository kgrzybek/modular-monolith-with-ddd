using System.Threading.Tasks;

namespace CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing
{
    public interface IProbe
    {
        bool IsSatisfied();

        Task SampleAsync();

        string DescribeFailureTo();
    }
}