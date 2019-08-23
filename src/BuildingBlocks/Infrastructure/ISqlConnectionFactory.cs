using System.Data;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}