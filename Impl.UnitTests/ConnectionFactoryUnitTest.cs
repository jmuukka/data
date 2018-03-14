using System;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class ConnectionFactoryUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_Null_ThrowsException()
        {
            new ConnectionFactory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_NullCommandFactory_ThrowsException()
        {
            var dbConnectionFactory = Substitute.For<IDbConnectionFactory>();

            new ConnectionFactory(dbConnectionFactory, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_NullDbConnectionFactory_ThrowsException()
        {
            var commandFactory = Substitute.For<ICommandFactory>();

            new ConnectionFactory(null, commandFactory);
        }

        [TestMethod]
        public void Create_ConstructedOnlyWithIDbConnectionFactory_ReturnsExpectedConnection()
        {
            var dbConnectionFactory = Substitute.For<IDbConnectionFactory>();
            var sut = new ConnectionFactory(dbConnectionFactory);

            var actual = sut.Create();

            Assert.AreSame(typeof(Connection), actual.GetType());
        }

        [TestMethod]
        public void Create_ConstructedOnlyWithIDbConnectionFactory_ConnectionHasExpectedDbConnection()
        {
            var dbConnectionFactory = Substitute.For<IDbConnectionFactory>();
            var dbConnection = Substitute.For<IDbConnection>();
            dbConnectionFactory.Create().Returns(dbConnection);
            var sut = new ConnectionFactory(dbConnectionFactory);

            var actual = sut.Create();

            Assert.AreSame(dbConnection, actual.DbConnection);
        }

        [TestMethod]
        public void Create_ReturnsExpectedConnection()
        {
            var dbConnectionFactory = Substitute.For<IDbConnectionFactory>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new ConnectionFactory(dbConnectionFactory, commandFactory);

            var actual = sut.Create();

            Assert.AreSame(typeof(Connection), actual.GetType());
        }

        [TestMethod]
        public void Create_ConnectionHasExpectedDbConnection()
        {
            var dbConnectionFactory = Substitute.For<IDbConnectionFactory>();
            var dbConnection = Substitute.For<IDbConnection>();
            dbConnectionFactory.Create().Returns(dbConnection);
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new ConnectionFactory(dbConnectionFactory, commandFactory);

            var actual = sut.Create();

            Assert.AreSame(dbConnection, actual.DbConnection);
        }
    }
}
