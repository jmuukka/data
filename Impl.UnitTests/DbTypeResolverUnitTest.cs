using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class DbTypeResolverUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryResolve_Null_ThrowsException()
        {
            var sut = new DbTypeResolver();

            sut.TryResolve(null);
        }

        [TestMethod]
        public void TryResolve_ReferenceTypes()
        {
            var sut = new DbTypeResolver();

            this.AssertEqual<byte[]>(sut, DbType.Binary);
            this.AssertEqual<string>(sut, DbType.String);
        }

        [TestMethod]
        public void TryResolve_ValueTypes()
        {
            var sut = new DbTypeResolver();

            this.AssertEqual<bool>(sut, DbType.Boolean);
            this.AssertEqual<byte>(sut, DbType.Byte);
            this.AssertEqual<DateTime>(sut, DbType.DateTime2);
            this.AssertEqual<DateTimeOffset>(sut, DbType.DateTimeOffset);
            this.AssertEqual<decimal>(sut, DbType.Decimal);
            this.AssertEqual<double>(sut, DbType.Double);
            this.AssertEqual<Guid>(sut, DbType.Guid);
            this.AssertEqual<short>(sut, DbType.Int16);
            this.AssertEqual<int>(sut, DbType.Int32);
            this.AssertEqual<long>(sut, DbType.Int64);
            this.AssertEqual<sbyte>(sut, DbType.SByte);
            this.AssertEqual<float>(sut, DbType.Single);
            this.AssertEqual<ushort>(sut, DbType.UInt16);
            this.AssertEqual<uint>(sut, DbType.UInt32);
            this.AssertEqual<ulong>(sut, DbType.UInt64);
        }

        [TestMethod]
        public void TryResolve_NullableValueTypes()
        {
            var sut = new DbTypeResolver();

            this.AssertEqual<Nullable<bool>>(sut, DbType.Boolean);
            this.AssertEqual<Nullable<byte>>(sut, DbType.Byte);
            this.AssertEqual<Nullable<DateTime>>(sut, DbType.DateTime2);
            this.AssertEqual<Nullable<DateTimeOffset>>(sut, DbType.DateTimeOffset);
            this.AssertEqual<Nullable<decimal>>(sut, DbType.Decimal);
            this.AssertEqual<Nullable<double>>(sut, DbType.Double);
            this.AssertEqual<Nullable<Guid>>(sut, DbType.Guid);
            this.AssertEqual<Nullable<short>>(sut, DbType.Int16);
            this.AssertEqual<Nullable<int>>(sut, DbType.Int32);
            this.AssertEqual<Nullable<long>>(sut, DbType.Int64);
            this.AssertEqual<Nullable<sbyte>>(sut, DbType.SByte);
            this.AssertEqual<Nullable<float>>(sut, DbType.Single);
            this.AssertEqual<Nullable<ushort>>(sut, DbType.UInt16);
            this.AssertEqual<Nullable<uint>>(sut, DbType.UInt32);
            this.AssertEqual<Nullable<ulong>>(sut, DbType.UInt64);
        }

        void AssertEqual<T>(IDbTypeResolver sut, DbType expected)
        {
            var result = sut.TryResolve(typeof(T));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryResolve_Unsupprted_ReturnsNull()
        {
            var sut = new DbTypeResolver();

            this.AssertNull<DbTypeResolver>(sut);
            this.AssertNull<object>(sut);
        }

        [TestMethod]
        public void TryResolve_Enumerable_ReturnsNull()
        {
            var sut = new DbTypeResolver();

            this.AssertNull<IEnumerable>(sut);
            this.AssertNull<IEnumerable<int>>(sut);
        }

        void AssertNull<T>(IDbTypeResolver sut)
        {
            var result = sut.TryResolve(typeof(T));

            Assert.AreEqual(null, result);
        }
    }
}
