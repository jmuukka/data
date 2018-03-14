using System;
using System.Globalization;

namespace Mutex.Data
{
    /// <summary>
    /// Represents a Convert helper.
    /// </summary>
#if DEBUG
    public
#endif
    static class ConvertHelper
    {
        /// <summary>
        /// Changes the type of the object.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="value">The object to convert.</param>
        /// <returns>
        /// A value which is equivalent with input value.
        /// </returns>
        public static T ChangeType<T>(object value)
        {
            // TODO or use without third param?
            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture); // TODO config for culture
        }
    }
}
