using System;

namespace Mutex.Data
{
    public partial interface IRecord
    {
        /// <summary>
        /// Gets the value of the named field as bool.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        bool GetBoolean(string name);

        /// <summary>
        /// Gets the value of the named field as DateTime.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        DateTime GetDateTime(string name);

        /// <summary>
        /// Gets the value of the named field as decimal.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        decimal GetDecimal(string name);

        /// <summary>
        /// Gets the value of the named field as double.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        double GetDouble(string name);

        /// <summary>
        /// Gets the value of the named field as float.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        float GetFloat(string name);

        /// <summary>
        /// Gets the value of the named field as Guid.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        Guid GetGuid(string name);

        /// <summary>
        /// Gets the value of the named field as Int16.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        short GetInt16(string name);

        /// <summary>
        /// Gets the value of the named field as Int32.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        int GetInt32(string name);

        /// <summary>
        /// Gets the value of the named field as Int64.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        long GetInt64(string name);

        /// <summary>
        /// Gets the value of the named field as string.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        string GetString(string name);

        /// <summary>
        /// Gets the value of the named field as byte array.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        byte[] GetBytes(string name);

        /// <summary>
        /// Gets the value of the named field as object.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        object GetValue(string name);

        /// <summary>
        /// Gets the value of the named field.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        T Get<T>(string name);
    }
}
