using System;
using System.Data;

namespace Mutex.Data
{
    public partial interface IParameterCollection
    {
        /// <summary>
        /// Add a bool parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, bool value);

        /// <summary>
        /// Add a DateTime parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, DateTime value);

        /// <summary>
        /// Add a decimal parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, decimal value);

        /// <summary>
        /// Add a double parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, double value);

        /// <summary>
        /// Add a float parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, float value);

        /// <summary>
        /// Add a Guid parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, Guid value);

        /// <summary>
        /// Add a short parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, short value);

        /// <summary>
        /// Add an int parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, int value);

        /// <summary>
        /// Add a long parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, long value);
    }
}
