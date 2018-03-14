using System;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Provides an interface to database connection with execute and query functionality.
    /// </summary>
    public interface IConnection : IExecutable, IQueryable, IDisposable
    {
        /// <summary>
        /// Get the underlying IDbConnection.
        /// </summary>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// Create an ICommand object.
        /// </summary>
        /// <returns>An ICommand object.</returns>
        ICommand CreateCommand();
    }
}
