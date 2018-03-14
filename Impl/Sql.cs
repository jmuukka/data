using System;
using System.Collections.Generic;

namespace Mutex.Data
{
    /// <summary>
    /// Represents an easy to use class for executing and querying SQL.
    /// </summary>
    /// <remarks>
    /// Each method will create a connection, does the job using a command and then closes the connection.
    /// </remarks>
    public class Sql : ISql, IExecutable, IQueryable
    {
        /// <summary>
        /// The factory which creates IConnection objects.
        /// </summary>
        protected readonly IConnectionFactory ConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the Sql class.
        /// </summary>
        /// <param name="connectionFactory">The IConnectionFactory to use.</param>
        /// <exception cref="ArgumentNullException">The connectionFactory was null.</exception>
        public Sql(IConnectionFactory connectionFactory)
        {
            this.ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        /// <summary>
        /// Executes an SQL text command.
        /// </summary>
        /// <param name="commandText">An SQL command text to execute.</param>
        /// <returns>A number of rows affected.</returns>
        public int Execute(string commandText)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                return connection.Execute(commandText);
            }
        }

        /// <summary>
        /// Executes an SQL text command with parameters.
        /// </summary>
        /// <param name="commandText">An SQL command text to execute.</param>
        /// <param name="parameters">An object which has command parameters in object's properties. See remarks.</param>
        /// <returns>A number of rows affected.</returns>
        /// <remarks>
        /// When the SQL statement is e.g. "update Product set Name = @Name where Id = @Id"
        /// then the parameter could be an anonymous object: new { Name = name, Id = id }
        /// or instance of Product class when the product object contains the data needed in SQL command.
        /// When the Product class has more properties than there are in SQL then they are added to Command's Parameters collection, but never used.
        /// Just make sure that bind names in SQL are same as property names are in the parameters object.
        /// </remarks>
        public int Execute(string commandText,
            object parameters)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                return connection.Execute(commandText, parameters);
            }
        }

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
        public ICollection<T> Query<T>(string commandText,
            object parameters,
            Func<ICollection<T>> createCollection,
            Func<IRecord, T> read)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                return connection.Query<T>(commandText, parameters, createCollection, read);
            }
        }

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
        public ICollection<T> Query<T>(string commandText,
            object parameters,
            Func<IRecord, T> read)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                return connection.Query<T>(commandText, parameters, read);
            }
        }

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
        public ICollection<T> Query<T>(string commandText,
            object parameters)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                return connection.Query<T>(commandText, parameters);
            }
        }
    }
}
