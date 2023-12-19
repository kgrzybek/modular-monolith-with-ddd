using System.Data;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Data
{
    /// <summary>
    /// Represents a factory for creating and managing SQL connections.
    /// </summary>
    public interface ISqlConnectionFactory
    {
        /// <summary>
        /// Gets an open SQL connection.
        /// </summary>
        /// <returns>An open instance of <see cref="IDbConnection"/>.</returns>
        IDbConnection GetOpenConnection();

        /// <summary>
        /// Creates a new SQL connection.
        /// </summary>
        /// <returns>A new instance of <see cref="IDbConnection"/>.</returns>
        IDbConnection CreateNewConnection();

        /// <summary>
        /// Gets the connection string used for establishing SQL connections.
        /// </summary>
        /// <returns>The connection string.</returns>
        string GetConnectionString();
    }
}