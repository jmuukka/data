using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class ConnectionUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_Null_ThrowsException()
        {
            new Connection(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_NullCommandFactory_ThrowsException()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            new Connection(dbConnection, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_NullDbConnection_ThrowsException()
        {
            var commandFactory = Substitute.For<ICommandFactory>();
            new Connection(null, commandFactory);
        }

        [TestMethod]
        public void Construct_DbConnectionIsExpectedInstance()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);

            Assert.AreSame(dbConnection, sut.DbConnection);
        }

        [TestMethod]
        public void Construct_ConstructWithoutICommandFactory_DbConnectionIsExpectedInstance()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var sut = new Connection(dbConnection);

            Assert.AreSame(dbConnection, sut.DbConnection);
        }

        [TestMethod]
        public void CreateCommand_ReturnsExpectedCommand()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var dbCommand = Substitute.For<IDbCommand>();
            dbConnection.CreateCommand().Returns(dbCommand);
            var commandFactory = Substitute.For<ICommandFactory>();
            var command = Substitute.For<ICommand>();
            commandFactory.Create(dbCommand).Returns(command);
            var sut = new Connection(dbConnection, commandFactory);

            var actual = sut.CreateCommand();

            Assert.AreSame(command, actual);
        }

        [TestMethod]
        public void CreateCommand_ConstructWithoutICommandFactory_ReturnsExpectedDbCommand()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var dbCommand = Substitute.For<IDbCommand>();
            dbConnection.CreateCommand().Returns(dbCommand);
            var sut = new Connection(dbConnection);

            var actual = sut.CreateCommand();

            Assert.AreSame(dbCommand, actual.DbCommand);
        }

        [TestMethod]
        public void Dispose_DbConnectionDisposed()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            IConnection sut = new Connection(dbConnection, commandFactory);

            sut.Dispose();

            dbConnection.Received(1).Dispose();
        }

        [TestMethod]
        public void Execute_WithoutParameters_DoesNotOpenCloseDisposeConnection()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);

            var actual = sut.Execute("drop db");

            dbConnection.Received(0).Open();
            dbConnection.Received(0).Close();
            dbConnection.Received(0).Dispose();
        }

        [TestMethod]
        public void Execute_WithoutParameters_CommandTextWasSet_ExecuteNonQueryWasCalled_CommandWasDisposed()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);
            var command = Substitute.For<ICommand>();
            commandFactory.Create(Arg.Any<IDbCommand>()).Returns(command);

            var actual = sut.Execute("drop db");

            Assert.AreEqual("drop db", command.CommandText);
            command.Received(1).ExecuteNonQuery();
            command.Received(1).Dispose();
        }

        [TestMethod]
        public void Execute_WithParameters_DoesNotOpenCloseDisposeConnection()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);

            var actual = sut.Execute("drop db", new { id = 1 });

            dbConnection.Received(0).Open();
            dbConnection.Received(0).Close();
            dbConnection.Received(0).Dispose();
        }

        [TestMethod]
        public void Execute_WithParameters_CommandTextWasSet_ExecuteNonQueryWasCalled_CommandWasDisposed_ParametersWereSet()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);
            var command = Substitute.For<ICommand>();
            commandFactory.Create(Arg.Any<IDbCommand>()).Returns(command);

            var actual = sut.Execute("drop db", new { id = 1, name = "n" });

            Assert.AreEqual("drop db", command.CommandText);
            command.Parameters.Received(1).Add("id", 1, DbType.Int32);
            command.Parameters.Received(1).Add("name", "n", DbType.String);
            command.Received(1).ExecuteNonQuery();
            command.Received(1).Dispose();
        }

        //[TestMethod]
        //public void Execute_WithObjectWithObject_ParametersWereSet()
        //{
        //    var dbConnection = Substitute.For<IDbConnection>();
        //    var commandFactory = Substitute.For<ICommandFactory>();
        //    var sut = new Connection(dbConnection, commandFactory);
        //    var command = Substitute.For<ICommand>();
        //    commandFactory.Create(Arg.Any<IDbCommand>()).Returns(command);

        //    var actual = sut.Execute("update", new Customer() { Address = new Address() { CountryCode2 = "FI" } });

        //    command.Parameters.Received(1).Add("CountryCode2", "FI", DbType.String);
        //}

        //[TestMethod]
        //public void Execute_WithObjectWithNullObject_ParametersWereNotSet()
        //{
        //    var dbConnection = Substitute.For<IDbConnection>();
        //    var commandFactory = Substitute.For<ICommandFactory>();
        //    var sut = new Connection(dbConnection, commandFactory);
        //    var command = Substitute.For<ICommand>();
        //    commandFactory.Create(Arg.Any<IDbCommand>()).Returns(command);

        //    var actual = sut.Execute("update", new Customer() { Address = null });

        //    command.Parameters.Received(1).Add("CountryCode2", null, DbType.String);
        //    //Assert.AreEqual(0, command.Parameters.ReceivedCalls().Count());
        //}

        [TestMethod]
        public void Execute_WithNullParameters_CommandTextWasSet_ExecuteNonQueryWasCalled_CommandWasDisposed_ParametersWereNotSet()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);
            var command = Substitute.For<ICommand>();
            commandFactory.Create(Arg.Any<IDbCommand>()).Returns(command);

            var actual = sut.Execute("drop db", null);

            Assert.AreEqual("drop db", command.CommandText);
            Assert.AreEqual(0, command.Parameters.ReceivedCalls().Count());
            command.Received(1).ExecuteNonQuery();
            command.Received(1).Dispose();
        }

        [TestMethod]
        public void Query_WithParameter_WithCreateCollection_WithCreateAndReadObject_DoesNotOpenCloseDisposeConnection()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);

            var actual = sut.Query<IProduct>("select * from db",
                new { id = 1 },
                () => new List<IProduct>(),
                record => new Product());

            dbConnection.Received(0).Open();
            dbConnection.Received(0).Close();
            dbConnection.Received(0).Dispose();
        }

        [TestMethod]
        public void Query_WithParameter_WithCreateCollection_WithCreateAndReadObject_CommandTextWasSet_ExecuteReaderWasCalled_CommandWasDisposed_ParametersWereSet()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);
            var command = Substitute.For<ICommand>();
            commandFactory.Create(Arg.Any<IDbCommand>()).Returns(command);
            var createCollection = new Func<ICollection<IProduct>>(() => new List<IProduct>());
            var createObject = new Func<IRecord, IProduct>(record => new Product());

            var actual = sut.Query<IProduct>("select * from db",
                new { id = 1 },
                createCollection,
                createObject);

            Assert.AreEqual("select * from db", command.CommandText);
            command.Parameters.Received(1).Add("id", 1, DbType.Int32);
            command.Received(1).ExecuteReader<IProduct>(createCollection, createObject);
            command.Received(1).Dispose();
        }

        [TestMethod]
        public void Query_WithParameter_WithCreateAndReadObject_CommandTextWasSet_ExecuteReaderWasCalled_CommandWasDisposed_ParametersWereSet()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);
            var command = Substitute.For<ICommand>();
            commandFactory.Create(Arg.Any<IDbCommand>()).Returns(command);
            var createObject = new Func<IRecord, IProduct>(record => new Product());

            var actual = sut.Query<IProduct>("select * from db",
                new { id = 1 },
                createObject);

            Assert.AreEqual("select * from db", command.CommandText);
            command.Parameters.Received(1).Add("id", 1, DbType.Int32);
            command.Received(1).ExecuteReader<IProduct>(Arg.Any<Func<ICollection<IProduct>>>(), createObject);
            command.Received(1).Dispose();
        }

        [TestMethod]
        public void Query_WithParameter_CommandTextWasSet_ExecuteReaderWasCalled_CommandWasDisposed_ParametersWereSet()
        {
            var dbConnection = Substitute.For<IDbConnection>();
            var commandFactory = Substitute.For<ICommandFactory>();
            var sut = new Connection(dbConnection, commandFactory);
            var command = Substitute.For<ICommand>();
            commandFactory.Create(Arg.Any<IDbCommand>()).Returns(command);

            var actual = sut.Query<IProduct>("select * from db",
                new { id = 1 });

            Assert.AreEqual("select * from db", command.CommandText);
            command.Parameters.Received(1).Add("id", 1, DbType.Int32);
            command.Received(1).ExecuteReader<IProduct>(Arg.Any<Func<ICollection<IProduct>>>(), Arg.Any<Func<IRecord, IProduct>>());
            command.Received(1).Dispose();
        }
    }
}
