using System;
using System.Collections.Generic;

namespace Mutex.Data
{
    /// <summary>
    /// Provides an interface for querying data.
    /// </summary>
    /// <remarks>
    /// The implementation is expected to create a command, use it and then dispose it.
    /// The implementation may create a connection, use it and then dispose it, or use an existing connection depending implementation.
    /// </remarks>
    public interface IQueryable
    {
        /// <summary>
        /// Queries all rows from the first result set. Use this overload when you want to control creating the collection, creating the object and setting the values to the object. This is the fastest overload.
        /// </summary>
        /// <typeparam name="T">The type of the entity to read.</typeparam>
        /// <param name="commandText">An SQL command text to execute.</param>
        /// <param name="parameters">An object which has command parameters in object's properties. See remarks.</param>
        /// <param name="createCollection">The method which creates the collection of type T objects.</param>
        /// <param name="read">The method which creates an object of type T and reads the values from the record and fills the object with read values.</param>
        /// <returns>A collection of type T objects.</returns>
        /// <remarks>
        /// When the SQL statement is e.g. "select * from Product where Id = @Id"
        /// then the parameter could be an anonymous object: new { Id = id }
        /// or instance of Product class when the product object contains the data needed in SQL command.
        /// When the Product class has more properties than there are in SQL then they are added to Command's Parameters collection, but never used.
        /// Just make sure that bind names in SQL are same as property names are in the parameters object.
        /// </remarks>
        ICollection<T> Query<T>(string commandText,
            object parameters,
            Func<ICollection<T>> createCollection,
            Func<IRecord, T> read);

        /// <summary>
        /// Queries all rows from the first result set. Use this overload when you want easy to use method and accept small performance penalty and the type T requires parameter in its constructor.
        /// </summary>
        /// <typeparam name="T">The type of the entity to read.</typeparam>
        /// <param name="commandText">An SQL command text to execute.</param>
        /// <param name="parameters">An object which has command parameters in object's properties. See remarks.</param>
        /// <param name="read">The method which creates an object of type T and reads the values from the record and fills the object with read values.</param>
        /// <returns>A collection of type T objects.</returns>
        /// <remarks>
        /// When the SQL statement is e.g. "select * from Product where Id = @Id"
        /// then the parameter could be an anonymous object: new { Id = id }
        /// or instance of Product class when the product object contains the data needed in SQL command.
        /// When the Product class has more properties than there are in SQL then they are added to Command's Parameters collection, but never used.
        /// Just make sure that bind names in SQL are same as property names are in the parameters object.
        /// 
        /// The method will read values from the result set rows and set the values of the matching properties.
        /// </remarks>
        ICollection<T> Query<T>(string commandText,
            object parameters,
            Func<IRecord, T> read);

        /// <summary>
        /// Queries all rows from the first result set. Use this overload when you want easy to use method and accept small performance penalty. The type T must have parameterless constructor.
        /// </summary>
        /// <typeparam name="T">The type of the entity to read.</typeparam>
        /// <param name="commandText">An SQL command text to execute.</param>
        /// <param name="parameters">An object which has command parameters in object's properties. See remarks.</param>
        /// <returns>A collection of type T objects.</returns>
        /// <remarks>
        /// When the SQL statement is e.g. "select * from Product where Id = @Id"
        /// then the parameter could be an anonymous object: new { Id = id }
        /// or instance of Product class when the product object contains the data needed in SQL command.
        /// When the Product class has more properties than there are in SQL then they are added to Command's Parameters collection, but never used.
        /// Just make sure that bind names in SQL are same as property names are in the parameters object.
        /// 
        /// The method will read values from the result set rows and set the values of the matching properties.
        /// </remarks>
        ICollection<T> Query<T>(string commandText,
            object parameters);
    }
}
