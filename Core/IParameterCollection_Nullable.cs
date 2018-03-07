using System;
using System.Data;

namespace Mutex.Data
{
    public partial interface IParameterCollection
    {
        /// <summary>
        /// Add a nullable bool parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, bool? value);

        /// <summary>
        /// Add a nullable DateTime parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, DateTime? value);

        /// <summary>
        /// Add a nullable decimal parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, decimal? value);

        /// <summary>
        /// Add a nullable double parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, double? value);

        /// <summary>
        /// Add a nullable float parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, float? value);

        /// <summary>
        /// Add a nullable Guid parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, Guid? value);

        /// <summary>
        /// Add a nullable short parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, short? value);

        /// <summary>
        /// Add a nullable int parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, int? value);

        /// <summary>
        /// Add a nullable long parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An IDbDataParameter object.</returns>
        IDbDataParameter Add(string name, long? value);
    }
}
