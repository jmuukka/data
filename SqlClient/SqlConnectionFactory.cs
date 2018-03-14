using System.Data;
using System.Data.SqlClient;

namespace Mutex.Data.SqlClient
{
    /// <summary>
    /// Represents a factory which creates SqlConnection objects.
    /// </summary>
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// Initializes a new SqlConnectionFactory instance.
        /// </summary>
        /// <remarks>
        /// Use this when you do not know the connection string at the time the factory is created.
        /// </remarks>
        public SqlConnectionFactory()
        {
        }

        /// <summary>
        /// Initializes a new SqlConnectionFactory instance using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to use when IDbConnection will be created.</param>
        public SqlConnectionFactory(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Get or set the connection string used for the connection objects.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Create an SqlConnection object. Will use the factory's connection string.
        /// </summary>
        /// <returns>An IDbConnection object.</returns>
        public virtual IDbConnection Create()
        {
            return new SqlConnection(this.ConnectionString);
        }
    }
}
