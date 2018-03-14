using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class CommandUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_Null_ThrowsException()
        {
            new Command(null);
        }

        [TestMethod]
        public void Construct_WithDbCommand_NoException()
        {
            var dbCommand = Substitute.For<IDbCommand>();

            new Command(dbCommand);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Construct_WithDbCommand_NullDbTypeResolver_ThrowsException()
        //{
        //    var dbCommand = Substitute.For<IDbCommand>();

        //    new Command(dbCommand, null);
        //}

        //[TestMethod]
        //public void Construct_WithDbCommand_WithDbTypeResolver_NoException()
        //{
        //    var dbCommand = Substitute.For<IDbCommand>();
        //    var dbTypeResolver = Substitute.For<IDbTypeResolver>();

        //    new Command(dbCommand, dbTypeResolver);
        //}

        [TestMethod]
        public void SetCommandText_GetCommandTextReturnsExpected_DbCommandCommandTextReturnsExpected()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);

            sut.CommandText = "exec";

            Assert.AreEqual("exec", sut.CommandText);
            Assert.AreEqual("exec", sut.DbCommand.CommandText);
        }

        [TestMethod]
        public void SetCommandType_GetCommandTypeReturnsExpected_DbCommandCommandTypeReturnsExpected()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);

            sut.CommandType = CommandType.StoredProcedure;

            Assert.AreEqual(CommandType.StoredProcedure, sut.CommandType);
            Assert.AreEqual(CommandType.StoredProcedure, sut.DbCommand.CommandType);
        }

        [TestMethod]
        public void ExecuteNonQuery_ConnectionOpen_DoesNotOpenOrCloseOrDisposeConnection_ReturnsExpectedValue()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Open);
            dbCommand.ExecuteNonQuery().Returns(12345);

            var actual = sut.ExecuteNonQuery();

            dbCommand.Connection.Received(0).Open();
            dbCommand.Connection.Received(0).Close();
            dbCommand.Connection.Received(0).Dispose();
            Assert.AreEqual(12345, actual);
        }

        [TestMethod]
        public void ExecuteNonQuery_ConnectionClosed_OpenAndCloseConnection_DoesNotDisposeConnection_ReturnsExpectedValue()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Closed);
            dbCommand.ExecuteNonQuery().Returns(12345);

            var actual = sut.ExecuteNonQuery();

            dbCommand.Connection.Received(1).Open();
            dbCommand.Connection.Received(1).Close();
            dbCommand.Connection.Received(0).Dispose();
            Assert.AreEqual(12345, actual);
        }

        [TestMethod]
        public void ExecuteScalar_ConnectionOpen_DoesNotOpenOrCloseOrDisposeConnection_ReturnsExpectedValue()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Open);
            dbCommand.ExecuteScalar().Returns((object)12345);

            var actual = sut.ExecuteScalar<int>();

            dbCommand.Connection.Received(0).Open();
            dbCommand.Connection.Received(0).Close();
            dbCommand.Connection.Received(0).Dispose();
            Assert.AreEqual(12345, actual);
        }

        [TestMethod]
        public void ExecuteScalar_ConnectionClosed_OpenAndCloseConnection_DoesNotDisposeConnection_ReturnsExpectedValue()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Closed);
            dbCommand.ExecuteScalar().Returns((object)12345);

            var actual = sut.ExecuteScalar<int>();

            dbCommand.Connection.Received(1).Open();
            dbCommand.Connection.Received(1).Close();
            dbCommand.Connection.Received(0).Dispose();
            Assert.AreEqual(12345, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteReaderFirstRow_Null_ThrowsException()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);

            sut.ExecuteReaderFirstRow<IProduct>(null);
        }

        [TestMethod]
        public void ExecuteReaderFirstRow_ConnectionOpen_DoesNotOpenOrCloseOrDisposeConnection()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Open);

            var actual = sut.ExecuteReaderFirstRow<IProduct>(record => null);

            dbCommand.Connection.Received(0).Open();
            dbCommand.Connection.Received(0).Close();
            dbCommand.Connection.Received(0).Dispose();
        }

        [TestMethod]
        public void ExecuteReaderFirstRow_ConnectionClosed_OpenAndCloseConnection_DoesNotDisposeConnection()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Closed);

            var actual = sut.ExecuteReaderFirstRow<IProduct>(record => null);

            dbCommand.Connection.Received(1).Open();
            dbCommand.Connection.Received(1).Close();
            dbCommand.Connection.Received(0).Dispose();
        }

        [TestMethod]
        public void ExecuteReaderFirstRow_NoRows_ReturnsNull()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            var dataReader = Substitute.For<IDataReader>();
            dataReader.Read().Returns(false);
            dbCommand.ExecuteReader(CommandBehavior.SingleRow).Returns(dataReader);
            var read = new Func<IRecord, IProduct>(record => new Product());

            var actual = sut.ExecuteReaderFirstRow<IProduct>(read);

            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void ExecuteReaderFirstRow_HasRows_ReturnsObject()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            var dataReader = Substitute.For<IDataReader>();
            dataReader.Read().Returns(true);
            dbCommand.ExecuteReader(CommandBehavior.SingleRow).Returns(dataReader);
            var expected = new Product();
            var read = new Func<IRecord, IProduct>(record => expected);

            var actual = sut.ExecuteReaderFirstRow<IProduct>(read);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteReader_FirstResultSet_NullReadObject_ThrowsException()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);

            sut.ExecuteReader<IProduct>(null);
        }

        [TestMethod]
        public void ExecuteReader_FirstResultSet_ConnectionOpen_DoesNotOpenOrCloseOrDisposeConnection()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Open);

            var actual = sut.ExecuteReader<IProduct>(record => null);

            dbCommand.Connection.Received(0).Open();
            dbCommand.Connection.Received(0).Close();
            dbCommand.Connection.Received(0).Dispose();
        }

        [TestMethod]
        public void ExecuteReader_FirstResultSet_ConnectionClosed_OpenAndCloseConnection_DoesNotDisposeConnection()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Closed);

            var actual = sut.ExecuteReader<IProduct>(record => null);

            dbCommand.Connection.Received(1).Open();
            dbCommand.Connection.Received(1).Close();
            dbCommand.Connection.Received(0).Dispose();
        }

        [TestMethod]
        public void ExecuteReader_FirstResultSet_ReturnsObjects()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            var dataReader = Substitute.For<IDataReader>();
            dataReader.Read().Returns(true, true, false);
            dbCommand.ExecuteReader(CommandBehavior.SingleResult).Returns(dataReader);
            var expected = new Product();

            var actual = sut.ExecuteReader<IProduct>(record => expected);

            Assert.AreEqual(2, actual.Count());
            Assert.AreSame(expected, actual.First());
            Assert.AreSame(expected, actual.Last());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteReader_FirstResultSet_WithCreateCollection_NullCreateCollection_ThrowsException()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);

            sut.ExecuteReader<IProduct>(null, record => new Product());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteReader_FirstResultSet_WithCreateCollection_NullReadObject_ThrowsException()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);

            sut.ExecuteReader<IProduct>(() => new List<IProduct>(), null);
        }

        [TestMethod]
        public void ExecuteReader_FirstResultSet_WithCreateCollection_ConnectionOpen_DoesNotOpenOrCloseOrDisposeConnection()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Open);

            var actual = sut.ExecuteReader<IProduct>(() => new List<IProduct>(), record => null);

            dbCommand.Connection.Received(0).Open();
            dbCommand.Connection.Received(0).Close();
            dbCommand.Connection.Received(0).Dispose();
        }

        [TestMethod]
        public void ExecuteReader_FirstResultSet_WithCreateCollection_ConnectionClosed_OpenAndCloseConnection_DoesNotDisposeConnection()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            dbCommand.Connection.State.Returns(ConnectionState.Closed);

            var actual = sut.ExecuteReader<IProduct>(() => new List<IProduct>(), record => null);

            dbCommand.Connection.Received(1).Open();
            dbCommand.Connection.Received(1).Close();
            dbCommand.Connection.Received(0).Dispose();
        }

        [TestMethod]
        public void ExecuteReader_FirstResultSet_WithCreateCollection_ReturnsObjects()
        {
            var dbCommand = Substitute.For<IDbCommand>();
            var sut = new Command(dbCommand);
            var dataReader = Substitute.For<IDataReader>();
            dataReader.Read().Returns(true, true, false);
            dbCommand.ExecuteReader(CommandBehavior.SingleResult).Returns(dataReader);
            var expected = new Product();

            var actual = sut.ExecuteReader<IProduct>(() => new List<IProduct>(), record => expected);

            Assert.AreEqual(2, actual.Count());
            Assert.AreSame(expected, actual.First());
            Assert.AreSame(expected, actual.Last());
        }
    }
}
