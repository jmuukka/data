using System;
using System.Data;

namespace Mutex.Data
{
    /// <summary>
    /// Represents a factory which creates Command objects.
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        /// <summary>
        /// Initializes a new instance of the CommandFactory class.
        /// </summary>
        public CommandFactory()
        {
        }

        /// <summary>
        /// Createa a Command object.
        /// </summary>
        /// <param name="dbCommand">The IDbCommand which is wrapped by Command.</param>
        /// <returns>An ICommand object.</returns>
        public virtual ICommand Create(IDbCommand dbCommand)
        {
            return new Command(dbCommand);
        }
    }
}
