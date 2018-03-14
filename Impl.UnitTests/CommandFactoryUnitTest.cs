using System;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class CommandFactoryUnitTest
    {
        [TestMethod]
        public void Construct_WithoutDbTypeResolver()
        {
            new CommandFactory();
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void Construct_WithNullDbTypeResolver_ThrowsException()
        //{
        //    new CommandFactory(null);
        //}

        //[TestMethod]
        //public void Construct_WithDbTypeResolver()
        //{
        //    var dbTypeResolver = Substitute.For<IDbTypeResolver>();
        //    new CommandFactory(dbTypeResolver);
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullDbCommand_ThrowsException()
        {
            var sut = new CommandFactory();

            sut.Create(null);
        }

        [TestMethod]
        public void Create_WithoutDbTypeResolver_CreatesCommand()
        {
            var sut = new CommandFactory();
            var dbCommand = Substitute.For<IDbCommand>();

            var actual = sut.Create(dbCommand);

            Assert.AreSame(typeof(Command), actual.GetType());
        }

        //[TestMethod]
        //public void Create_WithDbTypeResolver_CreatesCommand()
        //{
        //    var dbTypeResolver = Substitute.For<IDbTypeResolver>();
        //    var sut = new CommandFactory(dbTypeResolver);
        //    var dbCommand = Substitute.For<IDbCommand>();

        //    var actual = sut.Create(dbCommand);

        //    Assert.AreSame(typeof(Command), actual.GetType());
        //}
    }
}
