namespace Mutex.Data
{
    /// <summary>
    /// Provides an interface for creating IConnection objects.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Creates an object which implements IConnection.
        /// </summary>
        /// <returns>An IConnection object.</returns>
        IConnection Create();
    }
}
