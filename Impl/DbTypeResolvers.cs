using System;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Represents a static (Shared in Visual Basic) collection of IDbTypeResolver objects implemented by DbTypeResolverCollection.
    /// </summary>
    public static class DbTypeResolvers
    {
        static readonly DbTypeResolverCollection _resolvers;

        static DbTypeResolvers()
        {
            _resolvers = new DbTypeResolverCollection
            {
                new DbTypeResolver()
            };
        }

        /// <summary>
        /// Returns the instance of the DbTypeResolverCollection.
        /// </summary>
        public static DbTypeResolverCollection Instance
        {
            get { return _resolvers; }
        }
    }
}
