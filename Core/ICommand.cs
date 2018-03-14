using System;
using System.Collections.Generic;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Provides an interface to database command.
    /// </summary>
    public interface ICommand : IDisposable
    {
        /// <summary>
        /// Get the underlying IDbCommand.
        /// </summary>
        IDbCommand DbCommand { get; }

        /// <summary>
        /// Get or set the command text.
        /// </summary>
        string CommandText { get; set; }

        /// <summary>
        /// Get or set the CommandType of the command text.
        /// </summary>
        CommandType CommandType { get; set; }

        /// <summary>
        /// Get the parameters to the command.
        /// </summary>
        IParameterCollection Parameters { get; }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <returns>A number of rows affected.</returns>
        /// <remarks>
        /// When the connection is not open then opens the connection, executes the command and finally closes the connection.
        /// </remarks>
        int ExecuteNonQuery();

        /// <summary>
        /// Execute the command and return the value in the first column of the first row returned from the executed command.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The value.</returns>
        /// <remarks>
        /// When the connection is not open then opens the connection, executes the command and finally closes the connection.
        /// </remarks>
        T ExecuteScalar<T>();

        /// <summary>
        /// Execute the command and read the first row in the first result set.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="read">The method which creates an object of type T and reads the values from the record and fills the object with read values.</param>
        /// <returns>An object of type T or default value of the type T (when the type is a reference type then it's null).</returns>
        /// <exception cref="ArgumentNullException">The read method was null.</exception>
        /// <remarks>
        /// When the connection is not open then opens the connection, executes the command and finally closes the connection.
        /// </remarks>
        T ExecuteReaderFirstRow<T>(Func<IRecord, T> read);

        /// <summary>
        /// Execute the command and read all rows in the first result set.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="read">The method which creates an object of type T and reads the values from the record and fills the object with read values.</param>
        /// <returns>A collection of type T objects.</returns>
        /// <exception cref="ArgumentNullException">The read method was null.</exception>
        /// <remarks>
        /// When the connection is not open then opens the connection, executes the command and finally closes the connection.
        /// </remarks>
        ICollection<T> ExecuteReader<T>(Func<IRecord, T> read);

        /// <summary>
        /// Execute the command and read all rows in the first result set.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="createCollection">The method which creates a collection for type T objects.</param>
        /// <param name="read">The method which creates an object of type T and reads the values from the record and fills the object with read values.</param>
        /// <returns>A collection of type T objects.</returns>
        /// <exception cref="ArgumentNullException">The createCollection or the read method was null.</exception>
        /// <remarks>
        /// When the connection is not open then opens the connection, executes the command and finally closes the connection.
        /// </remarks>
        ICollection<T> ExecuteReader<T>(Func<ICollection<T>> createCollection,
            Func<IRecord, T> read);
    }
}
