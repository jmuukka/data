namespace Mutex.Data
{
    /// <summary>
    /// Provides an easy to use interface for executing and querying SQL.
    /// </summary>
    /// <remarks>
    /// The implementation is expected to create a command, use it and then dispose it.
    /// The implementation may create a connection, use it and then dispose it, or use an existing connection depending implementation.
    /// </remarks>
    public interface ISql : IExecutable, IQueryable
    {
    }
}
