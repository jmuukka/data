using System;

namespace Mutex.Data
{
    /// <summary>
    /// Provides an interface for executing commands.
    /// </summary>
    /// <remarks>
    /// The implementation is expected to create a command, use it and then dispose it.
    /// The implementation may create a connection, use it and then dispose it, or use an existing connection depending implementation.
    /// </remarks>
    public interface IExecutable
    {
        /// <summary>
        /// Executes an SQL text command.
        /// </summary>
        /// <param name="commandText">An SQL command text to execute.</param>
        /// <returns>A number of rows affected.</returns>
        int Execute(string commandText);

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
        int Execute(string commandText, object parameters);
    }
}
