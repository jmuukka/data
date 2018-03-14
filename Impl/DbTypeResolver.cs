using System;
using System.Collections.Generic;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Represents a default DbType resolver.
    /// </summary>
    public class DbTypeResolver : IDbTypeResolver
    {
        readonly Dictionary<Type, DbType> Types;

        /// <summary>
        /// Initializes a new instance of the DbTypeResolver class.
        /// </summary>
        public DbTypeResolver()
        {
            this.Types = new Dictionary<Type, DbType>()
            {
                { typeof(bool), DbType.Boolean },
                { typeof(byte), DbType.Byte },
                { typeof(byte[]), DbType.Binary },
                { typeof(DateTime), DbType.DateTime2 },
                { typeof(DateTimeOffset), DbType.DateTimeOffset },
                { typeof(decimal), DbType.Decimal },
                { typeof(double), DbType.Double },
                { typeof(float), DbType.Single },
                { typeof(Guid), DbType.Guid },
                { typeof(Int16), DbType.Int16 },
                { typeof(Int32), DbType.Int32 },
                { typeof(Int64), DbType.Int64 },
                { typeof(sbyte), DbType.SByte },
                { typeof(string), DbType.String },
                { typeof(UInt16), DbType.UInt16 },
                { typeof(UInt32), DbType.UInt32 },
                { typeof(UInt64), DbType.UInt64 }
            };
        }

        /// <summary>
        /// Tries to resolve the DbType for provided type. When the type is nullable type then uses the underlying type.
        /// </summary>
        /// <param name="type">The type of the value.</param>
        /// <returns>A DbType value or null.</returns>
        /// <remarks>
        /// Examples:
        ///  returns DbType.String for typeof(string)
        ///  returns DbType.Decimal for typeof(decimal)
        ///  returns DbType.Int32 for typeof(int?)
        /// </remarks>
        public virtual DbType? TryResolve(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsGenericType && type.IsValueType)
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                if (underlyingType == null)
                {
                    return null;
                }

                type = underlyingType;
            }

            if (this.Types.TryGetValue(type, out DbType dbType))
            {
                return dbType;
            }

            return null;
        }
    }
}
