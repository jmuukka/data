using System;
using System.Collections.Generic;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Represents a collection of IDbTypeResolver objects. The class also implements IDbTypeResolver.
    /// </summary>
    public class DbTypeResolverCollection : List<IDbTypeResolver>, IDbTypeResolver
    {
        /// <summary>
        /// Tries to resolve the DbType for provided type.
        /// </summary>
        /// <param name="type">The type of the value.</param>
        /// <returns>A DbType value or null.</returns>
        /// <remarks>
        /// Uses each of the resolver in the collection and returns the DbType when one of the resolvers could resolve the DbType.
        /// </remarks>
        public virtual DbType? TryResolve(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            foreach (var resolver in this)
            {
                var dbType = resolver.TryResolve(type);
                if (dbType.HasValue)
                {
                    return dbType;
                }
            }

            return null;
        }
    }
}
