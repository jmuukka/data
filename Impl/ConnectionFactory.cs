using System;

namespace Mutex.Data
{
    /// <summary>
    /// Represents a factory which creates Connection objects.
    /// </summary>
    /// <remarks>
    /// This class does not set the ConnectionString of the IConnection.
    /// </remarks>
    public class ConnectionFactory : IConnectionFactory
    {
        /// <summary>
        /// The factory which creates IDbConnection objects.
        /// </summary>
        protected readonly IDbConnectionFactory DbConnectionFactory;

        /// <summary>
        /// The factory which creates ICommand objects.
        /// </summary>
        protected readonly ICommandFactory CommandFactory;

        /// <summary>
        /// Initializes a new instance of the ConnectionFactory class.
        /// </summary>
        /// <param name="dbConnectionFactory">The factory which creates IDbConnection objects.</param>
        /// <exception cref="ArgumentNullException">The dbConnectionFactory was null.</exception>
        public ConnectionFactory(IDbConnectionFactory dbConnectionFactory)
        {
            this.DbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }

        /// <summary>
        /// Initializes a new instance of the ConnectionFactory class.
        /// </summary>
        /// <param name="dbConnectionFactory">The factory which creates IDbConnection objects.</param>
        /// <param name="commandFactory">The factory which creates ICommand objects.</param>
        /// <exception cref="ArgumentNullException">The dbConnectionFactory or the commandFactory was null.</exception>
        public ConnectionFactory(IDbConnectionFactory dbConnectionFactory,
            ICommandFactory commandFactory)
        {
            this.DbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
            this.CommandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
        }

        /// <summary>
        /// Create an IConnection object.
        /// </summary>
        /// <returns>An IConnection object.</returns>
        public virtual IConnection Create()
        {
            var dbConnection = this.DbConnectionFactory.Create();
            if (this.CommandFactory != null)
            {
                return new Connection(dbConnection, this.CommandFactory);
            }
            return new Connection(dbConnection);
        }
    }
}
