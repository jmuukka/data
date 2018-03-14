using System;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Represents a ParameterCollection class.
    /// </summary>
    public class ParameterCollection : IParameterCollection
    {
        /// <summary>
        /// The the underlaying IDbCommand object.
        /// </summary>
        protected readonly IDbCommand DbCommand;

        /// <summary>
        /// Initializes a new instance of the ParameterCollection class.
        /// </summary>
        /// <param name="dbCommand">The IDbCommand object which hosts the parameter collection.</param>
        /// <exception cref="ArgumentNullException">The dbCommand was null.</exception>
        public ParameterCollection(IDbCommand dbCommand)
        {
            this.DbCommand = dbCommand ?? throw new ArgumentNullException(nameof(dbCommand));
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="dbType">The DbType of the value.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, object value, DbType dbType)
        {
            if (value != null)
            {
                return this.CreateAndAdd(name, value, dbType);
            }

            return this.CreateAndAdd(name, DBNull.Value, dbType);
        }

        /// <summary>
        /// Add an object to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, object value)
        {
            if (value != null)
            {
                var dbType = DbTypeResolvers.Instance.TryResolve(value.GetType());
                return this.CreateAndAdd(name, value, dbType ?? DbType.Object);
            }

            return this.CreateAndAdd(name, DBNull.Value, DbType.Object);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, byte[] value)
        {
            if (value != null)
            {
                return this.CreateAndAdd(name, value, DbType.Binary);
            }

            return this.CreateAndAdd(name, DBNull.Value, DbType.Binary);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, string value)
        {
            if (value != null)
            {
                return this.CreateAndAdd(name, value, DbType.String);
            }

            return this.CreateAndAdd(name, DBNull.Value, DbType.String);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, bool value)
        {
            return this.CreateAndAdd(name, value, DbType.Boolean);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, DateTime value)
        {
            return this.CreateAndAdd(name, value, DbType.DateTime2);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, decimal value)
        {
            return this.CreateAndAdd(name, value, DbType.Decimal);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, double value)
        {
            return this.CreateAndAdd(name, value, DbType.Double);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, float value)
        {
            return this.CreateAndAdd(name, value, DbType.Single);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, Guid value)
        {
            return this.CreateAndAdd(name, value, DbType.Guid);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, short value)
        {
            return this.CreateAndAdd(name, value, DbType.Int16);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, int value)
        {
            return this.CreateAndAdd(name, value, DbType.Int32);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, long value)
        {
            return this.CreateAndAdd(name, value, DbType.Int64);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, bool? value)
        {
            return this.CreateAndAdd(name, value, DbType.Boolean);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, DateTime? value)
        {
            return this.CreateAndAdd(name, value, DbType.DateTime2);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, decimal? value)
        {
            return this.CreateAndAdd(name, value, DbType.Decimal);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, double? value)
        {
            return this.CreateAndAdd(name, value, DbType.Double);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, float? value)
        {
            return this.CreateAndAdd(name, value, DbType.Single);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, Guid? value)
        {
            return this.CreateAndAdd(name, value, DbType.Guid);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, short? value)
        {
            return this.CreateAndAdd(name, value, DbType.Int16);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, int? value)
        {
            return this.CreateAndAdd(name, value, DbType.Int32);
        }

        /// <summary>
        /// Add a value to the parameter collection.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        public IDbDataParameter Add(string name, long? value)
        {
            return this.CreateAndAdd(name, value, DbType.Int64);
        }

        IDbDataParameter CreateAndAdd<T>(string name, T? value, DbType dbType)
            where T : struct
        {
            var param = this.Create(name, value, dbType);
            this.DbCommand.Parameters.Add(param);
            return param;
        }

        IDbDataParameter Create<T>(string name, T? value, DbType dbType)
            where T : struct
        {
            if (value.HasValue)
            {
                return this.Create(name, value.Value, dbType);
            }

            return this.Create(name, DBNull.Value, dbType);
        }

        IDbDataParameter CreateAndAdd(string name, object value, DbType dbType)
        {
            var param = this.Create(name, value, dbType);
            this.DbCommand.Parameters.Add(param);
            return param;
        }

        IDbDataParameter Create(string name, object value, DbType dbType)
        {
            var param = this.DbCommand.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            param.DbType = dbType;
            return param;
        }
    }
}
