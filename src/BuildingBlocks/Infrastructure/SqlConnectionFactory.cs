using System.Data;
using System.Data.SqlClient;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure
{
    /// <summary>
    /// Represents a factory for creating and managing SQL connections.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="SqlConnectionFactory"/> class with the specified connection string.
    /// </remarks>
    /// <param name="connectionString">The connection string to be used for creating SQL connections.</param>
    public sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString = connectionString;
        private SqlConnection _connection;

        /// <summary>
        /// Gets an open SQL connection. If a connection is not already open, a new connection is created and opened.
        /// </summary>
        /// <returns>An open SQL connection.</returns>
        public IDbConnection GetOpenConnection()
        {
            if (this._connection == null || this._connection.State != ConnectionState.Open)
            {
                this._connection = new SqlConnection(_connectionString);
                this._connection.Open();
            }

            return this._connection;
        }

        /// <summary>
        /// Creates a new SQL connection and opens it.
        /// </summary>
        /// <returns>A new SQL connection.</returns>
        public IDbConnection CreateNewConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        /// <summary>
        /// Gets the connection string used by this factory.
        /// </summary>
        /// <returns>The connection string.</returns>
        public string GetConnectionString()
        {
            return _connectionString;
        }

        /// <summary>
        /// Releases the resources used by the SQL connection, if it is open.
        /// </summary>
        public void Dispose()
        {
            if (this._connection != null && this._connection.State == ConnectionState.Open)
            {
                this._connection.Dispose();
            }
        }
    }
}