using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mutex.Data.SqlClient;

namespace Mutex.Data.Impl.Tests
{
    [TestClass]
    public class SqlTest
    {
        ISql CreateSut()
        {
            var dbConnectionFactory = new SqlConnectionFactory("server=(local); database=Mutex.Data; Integrated Security=SSPI;");
            var connectionFactory = new ConnectionFactory(dbConnectionFactory);
            return new Sql(connectionFactory);
        }

        [TestMethod]
        public void Execute_Insert_Update_Delete()
        {
            var sut = this.CreateSut();
            var test = new Test()
            {
                RequireString20 = "temporary",
                RequireDateTime = DateTime.UtcNow
            };

            var actual = sut.Execute("insert into Test(RequireString20, RequireDateTime) values (@RequireString20, @RequireDateTime)", test);
            Assert.AreEqual(1, actual);

            actual = sut.Execute("update Test set NullableString10 = @NullableString10 where RequireString20 = @RequireString20", new { NullableString10 = "value", RequireString20 = test.RequireString20 });
            Assert.AreEqual(1, actual);

            actual = sut.Execute("delete from Test where RequireString20 = @RequireString20", test);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Query()
        {
            var sut = this.CreateSut();

            var actual = sut.Query<Test>("select*from Test", null);

            Assert.IsTrue(actual.Count > 0);
        }
    }
}
