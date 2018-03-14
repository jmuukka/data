using System;

namespace Mutex.Data
{
    /// <summary>
    /// Represents an object factory.
    /// </summary>
#if DEBUG
    public
#endif
        static class ObjectFactory
    {
        /// <summary>
        /// Creates an instance of the specified type. The class should have parameterless constructor.
        /// </summary>
        /// <typeparam name="T">The type of the class to create.</typeparam>
        /// <returns>An instance of the type T.</returns>
        public static T CreateInstance<T>()
        {
            return (T)ObjectFactory.CreateInstanceInternal(typeof(T));
        }

        /// <summary>
        /// Creates an instance of the specified type. The class should have parameterless constructor.
        /// </summary>
        /// <param name="type">The type of the object.</param>
        /// <returns>An instance of the type.</returns>
        /// <exception cref="ArgumentNullException">The type was null.</exception>
        public static object CreateInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            return ObjectFactory.CreateInstanceInternal(type);
        }

        static object CreateInstanceInternal(Type type)
        {
            return Activator.CreateInstance(type, nonPublic: true); // TODO config to deny using non public parameterless constructor
        }
    }
}
