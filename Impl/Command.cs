using System;
using System.Collections.Generic;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Represents a Command class.
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// Initializes a new Command instance.
        /// </summary>
        /// <param name="dbCommand">The IDbCommand which is wrapped by this class.</param>
        /// <exception cref="ArgumentNullException">The dbCommand was null.</exception>
        public Command(IDbCommand dbCommand)
        {
            this.DbCommand = dbCommand ?? throw new ArgumentNullException(nameof(dbCommand));
            this.Parameters = new ParameterCollection(dbCommand);
        }

        /// <summary>
        /// Get the underlying IDbCommand object.
        /// </summary>
        public IDbCommand DbCommand { get; }

        /// <summary>
        /// Get or set the command text.
        /// </summary>
        public string CommandText
        {
            get { return this.DbCommand.CommandText; }
            set { this.DbCommand.CommandText = value; }
        }

        /// <summary>
        /// Get or set the CommandType of the command text.
        /// </summary>
        public CommandType CommandType
        {
            get { return this.DbCommand.CommandType; }
            set { this.DbCommand.CommandType = value; }
        }

        /// <summary>
        /// Get the parameters collection.
        /// </summary>
        public IParameterCollection Parameters { get; }

        /// <summary>
        /// Execute a non-query.
        /// </summary>
        /// <returns>A number of rows affected.</returns>
        /// <remarks>
        /// When the connection is not open then opens the connection, executes the command and finally closes the connection.
        /// </remarks>
        public int ExecuteNonQuery()
        {
            return this.Execute<int>(() => this.DbCommand.ExecuteNonQuery());
        }

        /// <summary>
        /// Executes a command and returns the value of the first column on the first row.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The value.</returns>
        /// <remarks>
        /// When the connection is not open then opens the connection, executes the command and finally closes the connection.
        /// </remarks>
        public T ExecuteScalar<T>()
        {
            object value = this.Execute<object>(() => this.DbCommand.ExecuteScalar());
            return ConvertHelper.ChangeType<T>(value);
        }

        /// <summary>
        /// Execute the command and read the first row.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="read">The method which will read the values from the record and creates an instance of type T.</param>
        /// <returns>An instance of the object T or null.</returns>
        /// <exception cref="ArgumentNullException">The read was null.</exception>
        /// <remarks>
        /// When the connection is not open then opens the connection, executes the command and finally closes the connection.
        /// </remarks>
        public T ExecuteReaderFirstRow<T>(Func<IRecord, T> read)
        {
            if (read == null)
            {
                throw new ArgumentNullException(nameof(read));
            }

            return this.Execute<T>(() => this.ExecuteReaderSingleRowInternal<T>(read));
        }

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
        public ICollection<T> ExecuteReader<T>(Func<IRecord, T> read)
        {
            if (read == null)
            {
                throw new ArgumentNullException(nameof(read));
            }

            var collection = new List<T>();
            this.Execute(() => this.ExecuteReaderInternal<T>(collection, read));
            return collection;
        }

        /// <summary>
        /// Execute the command and read all rows in the first result set.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="createCollection">The method which creates a collection of object T.</param>
        /// <param name="read">The method which creates an object of type T and read values from the record and sets them into the object.</param>
        /// <returns>A collection of objects of type T.</returns>
        /// <exception cref="ArgumentNullException">The create or the read was null.</exception>
        /// <remarks>
        /// When the connection is not open then opens the connection, executes the command and finally closes the connection.
        /// </remarks>
        public ICollection<T> ExecuteReader<T>(Func<ICollection<T>> createCollection, Func<IRecord, T> read)
        {
            if (createCollection == null)
            {
                throw new ArgumentNullException(nameof(createCollection));
            }
            if (read == null)
            {
                throw new ArgumentNullException(nameof(read));
            }

            var collection = createCollection();
            this.Execute(() => this.ExecuteReaderInternal<T>(collection, read));
            return collection;
        }

        void Execute(Action execute)
        {
            var connection = this.DbCommand.Connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
                try
                {
                    execute();
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                execute();
            }
        }

        T Execute<T>(Func<T> execute)
        {
            var connection = this.DbCommand.Connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
                try
                {
                    return execute();
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                return execute();
            }
        }

        T ExecuteReaderSingleRowInternal<T>(Func<IRecord, T> read)
        {
            using (var reader = this.DbCommand.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (reader.Read())
                {
                    var record = new Record(reader);
                    return read(record);
                }

                return default(T);
            }
        }

        void ExecuteReaderInternal<T>(ICollection<T> collection, Func<IRecord, T> read)
        {
            using (var reader = this.DbCommand.ExecuteReader(CommandBehavior.SingleResult))
            {
                var record = new Record(reader);

                while (reader.Read())
                {
                    collection.Add(read(record));
                }
            }
        }

        void IDisposable.Dispose()
        {
            this.DbCommand.Dispose();
        }
    }
}
