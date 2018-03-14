using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mutex.Data.SqlClient;

namespace Mutex.Data.Impl.Tests
{
    [TestClass]
    public class CommandTest
    {
        IConnectionFactory ConnectionFactory;

        public CommandTest()
        {
            var dbConnectionFactory = new SqlConnectionFactory("server=(local); database=Mutex.Data; Integrated Security=SSPI;");
            this.ConnectionFactory = new ConnectionFactory(dbConnectionFactory);
        }

        [TestMethod]
        public void ExecuteScalar()
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into Test(RequireString20, RequireDateTime) values (@RequireString20, @RequireDateTime); select scope_identity();";
                    command.Parameters.Add("RequireString20", "C1");
                    command.Parameters.Add("RequireDateTime", DateTime.UtcNow);

                    var actual = command.ExecuteScalar<int>();

                    Assert.IsTrue(actual > 0);
                }
            }
        }
    }
}
