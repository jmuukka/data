using System;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class DbTypeResolverCollectionUnitTest
    {
        [TestMethod]
        public void Construct_IsEmpty()
        {
            var sut = new DbTypeResolverCollection();

            var result = sut.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void AddOne_HasOne()
        {
            var sut = new DbTypeResolverCollection();
            sut.Add(new DbTypeResolver());

            var result = sut.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryResolve_Null_ThrowsException()
        {
            var sut = new DbTypeResolverCollection();

            sut.TryResolve(null);
        }

        [TestMethod]
        public void TryResolve_Known_HasDefaultDbTypeResolver_ReturnsExpectedDbType()
        {
            var sut = new DbTypeResolverCollection();
            sut.Add(new DbTypeResolver());

            var actual = sut.TryResolve(typeof(string));

            Assert.AreEqual(DbType.String, actual);
        }

        [TestMethod]
        public void TryResolve_Unknown_HasDefaultDbTypeResolver_ReturnsNull()
        {
            var sut = new DbTypeResolverCollection();
            sut.Add(new DbTypeResolver());

            var actual = sut.TryResolve(typeof(TimeSpan));

            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void TryResolve_Xml_HasDefaultDbTypeResolver_HasCustomDbTypeResolver_ReturnsXml()
        {
            var sut = new DbTypeResolverCollection();
            sut.Add(new DbTypeResolver());
            sut.Add(new CustomDbTypeResolver());

            var actual = sut.TryResolve(typeof(System.Xml.XmlDocument));

            Assert.AreEqual(DbType.Xml, actual);
        }

        class CustomDbTypeResolver : IDbTypeResolver
        {
            public DbType? TryResolve(Type type)
            {
                if (type == typeof(System.Xml.XmlDocument))
                {
                    return DbType.Xml;
                }

                return null;
            }
        }
    }
}
