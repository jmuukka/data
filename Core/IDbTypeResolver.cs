using System;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Provides an interface for resolving DbType.
    /// </summary>
    public interface IDbTypeResolver
    {
        /// <summary>
        /// Tries to resolve DbType of Type.
        /// </summary>
        /// <param name="type">The type of the data.</param>
        /// <returns>A DbType or null.</returns>
        /// <exception cref="ArgumentNullException">The type was null.</exception>
        DbType? TryResolve(Type type);
    }
}
