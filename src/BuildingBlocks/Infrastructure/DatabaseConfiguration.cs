using CompanyName.MyMeetings.BuildingBlocks.Application.Data;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure;

public class DatabaseConfiguration : IDatabaseConfiguration
{
    public DatabaseConfiguration(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public string ConnectionString { get; }
}