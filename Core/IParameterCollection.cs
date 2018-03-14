using System;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Provides an interface to parameters to the command.
    /// </summary>
    public partial interface IParameterCollection
    {
        /// <summary>
        /// Add a parameter of specific DbType.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="dbType">The DbType of the value.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, object value, DbType dbType);

        /// <summary>
        /// Add an object parameter. The implementation tries to resolve the DbType of the value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, object value);

        /// <summary>
        /// Add a byte array parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, byte[] value);

        /// <summary>
        /// Add a string parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, string value);
    }
}
