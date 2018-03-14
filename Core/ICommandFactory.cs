using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Provides an interface for creating ICommand objects.
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Creates an ICommand object.
        /// </summary>
        /// <param name="dbCommand">An IDbCommand object.</param>
        /// <returns>An ICommand object.</returns>
        ICommand Create(IDbCommand dbCommand);
    }
}
