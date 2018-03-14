using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class SqlUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_Null_ThrowsException()
        {
            new Sql(null);
        }

        [TestMethod]
        public void Execute_CommandText_ReceivesExpectedValue_ConnectionDisposed()
        {
            var connectionFactory = Substitute.For<IConnectionFactory>();
            var connection = this.CreateConnection(connectionFactory);
            var sut = new Sql(connectionFactory);
            var commandText = "delete from Product";
            connection.Execute(commandText).Returns(123);

            var actual = sut.Execute(commandText);

            Assert.AreEqual(123, actual);
            connection.Received(1).Dispose();
        }

        [TestMethod]
        public void Execute_CommandTextWithParameters_ReceivesExpectedValue_ConnectionDisposed()
        {
            var connectionFactory = Substitute.For<IConnectionFactory>();
            var connection = this.CreateConnection(connectionFactory);
            var sut = new Sql(connectionFactory);
            var commandText = "delete from Product";
            var parameters = new { id = 2 };
            connection.Execute(commandText, parameters).Returns(123);

            var actual = sut.Execute(commandText, parameters);

            Assert.AreEqual(123, actual);
            connection.Received(1).Dispose();
        }

        [TestMethod]
        public void Query_CommandTextWithParameters_ReceivesExpectedValue_ConnectionDisposed()
        {
            var connectionFactory = Substitute.For<IConnectionFactory>();
            var connection = this.CreateConnection(connectionFactory);
            var sut = new Sql(connectionFactory);
            var commandText = "delete from Product";
            var parameters = new { id = 2 };
            var expected = new IProduct[] { };
            connection.Query<IProduct>(commandText, parameters).Returns(expected);

            var actual = sut.Query<IProduct>(commandText, parameters);

            Assert.AreEqual(expected, actual);
            connection.Received(1).Dispose();
        }

        [TestMethod]
        public void Query_CommandTextWithParametersAndReader_ReceivesExpectedValue_ConnectionDisposed()
        {
            var connectionFactory = Substitute.For<IConnectionFactory>();
            var connection = this.CreateConnection(connectionFactory);
            var sut = new Sql(connectionFactory);
            var commandText = "delete from Product";
            var parameters = new { id = 2 };
            var expected = new IProduct[] { };
            var read = new Func<IRecord, IProduct>(record => null);
            connection.Query<IProduct>(commandText, parameters, read).Returns(expected);

            var actual = sut.Query<IProduct>(commandText, parameters, read);

            Assert.AreEqual(expected, actual);
            connection.Received(1).Dispose();
        }

        [TestMethod]
        public void Query_CommandTextWithParametersCreateCollectionAndResder_ReceivesExpectedValue_ConnectionDisposed()
        {
            var connectionFactory = Substitute.For<IConnectionFactory>();
            var connection = this.CreateConnection(connectionFactory);
            var sut = new Sql(connectionFactory);
            var commandText = "delete from Product";
            var parameters = new { id = 2 };
            var expected = new IProduct[] { };
            var createCollection = new Func<ICollection<IProduct>>(() => new List<IProduct>());
            var read = new Func<IRecord, IProduct>(record => null);
            connection.Query<IProduct>(commandText, parameters, createCollection, read).Returns(expected);

            var actual = sut.Query<IProduct>(commandText, parameters, createCollection, read);

            Assert.AreEqual(expected, actual);
            connection.Received(1).Dispose();
        }

        IConnection CreateConnection(IConnectionFactory connectionFactory)
        {
            var connection = Substitute.For<IConnection>();
            connectionFactory.Create().Returns(connection);
            return connection;
        }
    }
}
