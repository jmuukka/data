using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Provides an interface for creating IDbConnection objects.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates an object which implements IDbConnection.
        /// </summary>
        /// <returns>An IDbConnection object.</returns>
        IDbConnection Create();
    }
}
