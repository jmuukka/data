using System;

namespace Mutex.Data
{
    public partial interface IRecord
    {
        /// <summary>
        /// Gets the value of the named field as nullable bool.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        bool? GetNullableBoolean(string name);

        /// <summary>
        /// Gets the value of the named field as nullable DateTime.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        DateTime? GetNullableDateTime(string name);

        /// <summary>
        /// Gets the value of the named field as nullable decimal.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        decimal? GetNullableDecimal(string name);

        /// <summary>
        /// Gets the value of the named field as nullable double.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        double? GetNullableDouble(string name);

        /// <summary>
        /// Gets the value of the named field as nullable float.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        float? GetNullableFloat(string name);

        /// <summary>
        /// Gets the value of the named field as nullable Guid.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        Guid? GetNullableGuid(string name);

        /// <summary>
        /// Gets the value of the named field as nullable Int16.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        short? GetNullableInt16(string name);

        /// <summary>
        /// Gets the value of the named field as nullable Int32.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        int? GetNullableInt32(string name);

        /// <summary>
        /// Gets the value of the named field as nullable Int64.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        long? GetNullableInt64(string name);

        /// <summary>
        /// Gets the value of the specified field as nullable type.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        T? GetNullable<T>(string name)
            where T : struct;
    }
}
