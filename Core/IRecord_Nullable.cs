using System;

namespace Mutex.Data
{
    public partial interface IRecord
    {
        /// <summary>
        /// Gets the value of the specified field as nullable Boolean.
        /// </summary>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        bool? GetNullableBoolean(int i);

        /// <summary>
        /// Gets the value of the specified field as nullable DateTime.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>The value of the field or null.</returns>
        DateTime? GetNullableDateTime(int i);

        /// <summary>
        /// Gets the value of the specified field as nullable decimal.
        /// </summary>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        decimal? GetNullableDecimal(int i);

        /// <summary>
        /// Gets the value of the specified field as nullable double.
        /// </summary>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        double? GetNullableDouble(int i);

        /// <summary>
        /// Gets the value of the specified field as nullable float.
        /// </summary>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        float? GetNullableFloat(int i);

        /// <summary>
        /// Gets the value of the specified field as nullable Guid.
        /// </summary>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        Guid? GetNullableGuid(int i);

        /// <summary>
        /// Gets the value of the specified field as nullable Int16.
        /// </summary>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        short? GetNullableInt16(int i);

        /// <summary>
        /// Gets the value of the specified field as nullable Int32.
        /// </summary>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        int? GetNullableInt32(int i);

        /// <summary>
        /// Gets the value of the specified field as nullable Int64.
        /// </summary>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        long? GetNullableInt64(int i);

        /// <summary>
        /// Gets the value of the specified field as nullable type.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        T? GetNullable<T>(int i)
            where T : struct;
    }
}
