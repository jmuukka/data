using System;

namespace Mutex.Data
{
    /// <summary>
    /// Provides an extended interface to System.Data.IDataRecord.
    /// </summary>
    /// <remarks>
    /// This interface adds many methods which helps reading values from the record using name and it helps reading nullable types.
    /// </remarks>
    public partial interface IRecord : System.Data.IDataRecord
    {
        /// <summary>
        /// Gets the value of the field as byte array.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value of the field.</returns>
        byte[] GetBytes(int i);

        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field.</returns>
        T Get<T>(int i);

        /// <summary>
        /// Gets an object of type T by creating an instance of type T, setting the values of properties with values from the matching fields in the record.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <returns>A new instance of type T.</returns>
        /// <remarks>
        /// The type T must have a parameterless constructor.
        /// When the type T contains properties of other types then the values in the properties of them are also set.
        /// When the property is null then the instance will be created and set to the property. In this case the type must have parameterless constructor.
        /// When the property is not null then the values are set to the properties of existing instance.
        /// When the property is a collection then it will be ignored.
        /// This logic is recursive.
        /// </remarks>
        T Get<T>();

        /// <summary>
        /// Get the values of matching properties and fields in the record.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object which is the target.</param>
        /// <exception cref="ArgumentNullException">The obj was null.</exception>
        /// <remarks>
        /// When the type T contains properties of other types then the values in the properties of them are also set.
        /// When the property is null then the instance will be created and set to the property. In this case the type must have parameterless constructor.
        /// When the property is not null then the values are set to the properties of existing instance.
        /// When the property is a collection then it will be ignored.
        /// This logic is recursive.
        /// </remarks>
        void Get<T>(T obj);
    }
}
