using System;
using System.Data;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Mutex.Data.Impl.UnitTests
{
    [TestClass]
    public class RecordUnitTest
    {
        const string Name = "n";
        const int Index = 1;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_Null_ThrowsException()
        {
            new Record(null);
        }

        [TestMethod]
        public void GetBytes_Named_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            dataRecord.GetOrdinal(Name).Returns(Index);
            var sut = new Record(dataRecord);
            var bytes = new byte[] { 211, 34, 97 };
            dataRecord.GetBytes(Index, 0, null, 0, 0).Returns(bytes.Length);
            dataRecord.GetBytes(Index, 0, Arg.Any<byte[]>(), 0, bytes.Length).Returns(bytes.Length);
            dataRecord.When(s => s.GetBytes(Index, 0, Arg.Any<byte[]>(), 0, bytes.Length)).Do(info => { var b = info.Arg<byte[]>(); b[0] = bytes[0]; b[1] = bytes[1]; b[2] = bytes[2]; });

            var actual = sut.GetBytes(Name);

            Assert.IsTrue(bytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetBytes_Indexed_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);
            var bytes = new byte[] { 211, 34, 97 };
            dataRecord.GetBytes(Index, 0, null, 0, 0).Returns(bytes.Length);
            dataRecord.GetBytes(Index, 0, Arg.Any<byte[]>(), 0, bytes.Length).Returns(bytes.Length);
            dataRecord.When(s => s.GetBytes(Index, 0, Arg.Any<byte[]>(), 0, bytes.Length)).Do(info => { var b = info.Arg<byte[]>(); b[0] = bytes[0]; b[1] = bytes[1]; b[2] = bytes[2]; });

            var actual = sut.GetBytes(Index);

            Assert.IsTrue(bytes.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetNullable_Named_DBNull_ReturnsNull()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);
            dataRecord.GetOrdinal(Name).Returns(Index);
            dataRecord.IsDBNull(Index).Returns(true);

            Assert.AreEqual(null, sut.GetNullableBoolean(Name));
            Assert.AreEqual(null, sut.GetNullableDateTime(Name));
            Assert.AreEqual(null, sut.GetNullableDecimal(Name));
            Assert.AreEqual(null, sut.GetNullableDouble(Name));
            Assert.AreEqual(null, sut.GetNullableFloat(Name));
            Assert.AreEqual(null, sut.GetNullableGuid(Name));
            Assert.AreEqual(null, sut.GetNullableInt16(Name));
            Assert.AreEqual(null, sut.GetNullableInt32(Name));
            Assert.AreEqual(null, sut.GetNullableInt64(Name));
        }

        [TestMethod]
        public void GetNullable_Named_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);
            dataRecord.GetOrdinal(Name).Returns(Index);
            var @bool = true;
            var time = DateTime.UtcNow;
            var dec = 2.4m;
            var dbl = 5.33d;
            var flt = 2.44f;
            var guid = Guid.NewGuid();
            short @short = 21;
            var @int = 76;
            var @long = 84;

            dataRecord.GetBoolean(Index).Returns(@bool);
            dataRecord.GetDateTime(Index).Returns(time);
            dataRecord.GetDecimal(Index).Returns(dec);
            dataRecord.GetDouble(Index).Returns(dbl);
            dataRecord.GetFloat(Index).Returns(flt);
            dataRecord.GetGuid(Index).Returns(guid);
            dataRecord.GetInt16(Index).Returns(@short);
            dataRecord.GetInt32(Index).Returns(@int);
            dataRecord.GetInt64(Index).Returns(@long);

            Assert.AreEqual(@bool, sut.GetNullableBoolean(Name));
            Assert.AreEqual(time, sut.GetNullableDateTime(Name));
            Assert.AreEqual(dec, sut.GetNullableDecimal(Name));
            Assert.AreEqual(dbl, sut.GetNullableDouble(Name));
            Assert.AreEqual(flt, sut.GetNullableFloat(Name));
            Assert.AreEqual(guid, sut.GetNullableGuid(Name));
            Assert.AreEqual(@short, sut.GetNullableInt16(Name));
            Assert.AreEqual(@int, sut.GetNullableInt32(Name));
            Assert.AreEqual(@long, sut.GetNullableInt64(Name));
        }

        [TestMethod]
        public void GetNullable_Indexed_DBNull_ReturnsNull()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);
            dataRecord.IsDBNull(Index).Returns(true);

            Assert.AreEqual(null, sut.GetNullableBoolean(Index));
            Assert.AreEqual(null, sut.GetNullableDateTime(Index));
            Assert.AreEqual(null, sut.GetNullableDecimal(Index));
            Assert.AreEqual(null, sut.GetNullableDouble(Index));
            Assert.AreEqual(null, sut.GetNullableFloat(Index));
            Assert.AreEqual(null, sut.GetNullableGuid(Index));
            Assert.AreEqual(null, sut.GetNullableInt16(Index));
            Assert.AreEqual(null, sut.GetNullableInt32(Index));
            Assert.AreEqual(null, sut.GetNullableInt64(Index));
        }

        [TestMethod]
        public void GetNullable_Indexed_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);
            var @bool = true;
            var time = DateTime.UtcNow;
            var dec = 2.4m;
            var dbl = 5.33d;
            var flt = 2.44f;
            var guid = Guid.NewGuid();
            short @short = 21;
            var @int = 76;
            var @long = 84;

            dataRecord.GetBoolean(Index).Returns(@bool);
            dataRecord.GetDateTime(Index).Returns(time);
            dataRecord.GetDecimal(Index).Returns(dec);
            dataRecord.GetDouble(Index).Returns(dbl);
            dataRecord.GetFloat(Index).Returns(flt);
            dataRecord.GetGuid(Index).Returns(guid);
            dataRecord.GetInt16(Index).Returns(@short);
            dataRecord.GetInt32(Index).Returns(@int);
            dataRecord.GetInt64(Index).Returns(@long);

            Assert.AreEqual(@bool, sut.GetNullableBoolean(Index));
            Assert.AreEqual(time, sut.GetNullableDateTime(Index));
            Assert.AreEqual(dec, sut.GetNullableDecimal(Index));
            Assert.AreEqual(dbl, sut.GetNullableDouble(Index));
            Assert.AreEqual(flt, sut.GetNullableFloat(Index));
            Assert.AreEqual(guid, sut.GetNullableGuid(Index));
            Assert.AreEqual(@short, sut.GetNullableInt16(Index));
            Assert.AreEqual(@int, sut.GetNullableInt32(Index));
            Assert.AreEqual(@long, sut.GetNullableInt64(Index));
        }

        [TestMethod]
        public void Get_Named_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);
            dataRecord.GetOrdinal(Name).Returns(Index);
            var @bool = true;
            var time = DateTime.UtcNow;
            var dec = 2.4m;
            var dbl = 5.33d;
            var flt = 2.44f;
            var guid = Guid.NewGuid();
            short @short = 21;
            var @int = 76;
            var @long = 84;
            var str = "s";
            var obj = new object();
            var ordinal = 3;

            dataRecord.GetBoolean(Index).Returns(@bool);
            dataRecord.GetDateTime(Index).Returns(time);
            dataRecord.GetDecimal(Index).Returns(dec);
            dataRecord.GetDouble(Index).Returns(dbl);
            dataRecord.GetFloat(Index).Returns(flt);
            dataRecord.GetGuid(Index).Returns(guid);
            dataRecord.GetInt16(Index).Returns(@short);
            dataRecord.GetInt32(Index).Returns(@int);
            dataRecord.GetInt64(Index).Returns(@long);
            dataRecord.GetOrdinal("ord").Returns(ordinal);
            dataRecord.GetString(Index).Returns(str);
            dataRecord.GetValue(Index).Returns(obj);
            dataRecord[Name].Returns(obj);

            Assert.AreEqual(@bool, sut.GetBoolean(Name));
            Assert.AreEqual(time, sut.GetDateTime(Name));
            Assert.AreEqual(dec, sut.GetDecimal(Name));
            Assert.AreEqual(dbl, sut.GetDouble(Name));
            Assert.AreEqual(flt, sut.GetFloat(Name));
            Assert.AreEqual(guid, sut.GetGuid(Name));
            Assert.AreEqual(@short, sut.GetInt16(Name));
            Assert.AreEqual(@int, sut.GetInt32(Name));
            Assert.AreEqual(@long, sut.GetInt64(Name));
            Assert.AreEqual(ordinal, sut.GetOrdinal("ord"));
            Assert.AreEqual(str, sut.GetString(Name));
            Assert.AreEqual(obj, sut.GetValue(Name));
            Assert.AreEqual(obj, sut[Name]);
        }

        [TestMethod]
        public void Get_Named_DBNull_ReturnsNull()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            dataRecord.GetOrdinal(Name).Returns(Index);
            dataRecord.IsDBNull(Index).Returns(true);
            var sut = new Record(dataRecord);

            dataRecord.GetValue(Index).Returns(DBNull.Value);
            dataRecord[Name].Returns(DBNull.Value);

            Assert.IsNull(sut[Name]);
            Assert.IsNull(sut.GetString(Name));
            Assert.IsNull(sut.GetBytes(Name));
            Assert.IsNull(sut.GetValue(Name));
        }

        [TestMethod]
        public void Get_Indexed_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);
            var @bool = true;
            byte @byte = 4;
            var @char = '\t';
            var reader = Substitute.For<IDataReader>();
            var datatypeName = "dtn";
            var time = DateTime.UtcNow;
            var dec = 2.4m;
            var dbl = 5.33d;
            var type = typeof(IProduct);
            var flt = 2.44f;
            var guid = Guid.NewGuid();
            short @short = 21;
            var @int = 76;
            var @long = 84;
            var name = "n";
            var str = "s";
            var obj = new object();

            dataRecord.GetBoolean(Index).Returns(@bool);
            dataRecord.GetByte(Index).Returns(@byte);
            dataRecord.GetChar(Index).Returns(@char);
            dataRecord.GetData(Index).Returns(reader);
            dataRecord.GetDataTypeName(Index).Returns(datatypeName);
            dataRecord.GetDateTime(Index).Returns(time);
            dataRecord.GetDecimal(Index).Returns(dec);
            dataRecord.GetDouble(Index).Returns(dbl);
            dataRecord.GetFieldType(Index).Returns(type);
            dataRecord.GetFloat(Index).Returns(flt);
            dataRecord.GetGuid(Index).Returns(guid);
            dataRecord.GetInt16(Index).Returns(@short);
            dataRecord.GetInt32(Index).Returns(@int);
            dataRecord.GetInt64(Index).Returns(@long);
            dataRecord.GetName(Index).Returns(name);
            dataRecord.GetString(Index).Returns(str);
            dataRecord.GetValue(Index).Returns(obj);
            dataRecord[Index].Returns(obj);

            Assert.AreEqual(@bool, sut.GetBoolean(Index));
            Assert.AreEqual(@byte, sut.GetByte(Index));
            Assert.AreEqual(@char, sut.GetChar(Index));
            Assert.AreEqual(reader, sut.GetData(Index));
            Assert.AreEqual(datatypeName, sut.GetDataTypeName(Index));
            Assert.AreEqual(time, sut.GetDateTime(Index));
            Assert.AreEqual(dec, sut.GetDecimal(Index));
            Assert.AreEqual(dbl, sut.GetDouble(Index));
            Assert.AreEqual(type, sut.GetFieldType(Index));
            Assert.AreEqual(flt, sut.GetFloat(Index));
            Assert.AreEqual(guid, sut.GetGuid(Index));
            Assert.AreEqual(@short, sut.GetInt16(Index));
            Assert.AreEqual(@int, sut.GetInt32(Index));
            Assert.AreEqual(@long, sut.GetInt64(Index));
            Assert.AreEqual(name, sut.GetName(Index));
            Assert.AreEqual(obj, sut.GetValue(Index));
            Assert.AreEqual(str, sut.GetString(Index));
            Assert.AreEqual(obj, sut[Index]);
        }

        [TestMethod]
        public void Get_Indexed_DBNull_ReturnsNull()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            dataRecord.IsDBNull(Index).Returns(true);
            var sut = new Record(dataRecord);

            dataRecord[Index].Returns(DBNull.Value);

            Assert.IsNull(sut[Index]);
            Assert.IsNull(sut.GetBytes(Index));
            Assert.IsNull(sut.GetString(Index));
            Assert.IsNull(sut.GetValue(Index));
        }

        [TestMethod]
        public void FieldCount_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            dataRecord.FieldCount.Returns(44);
            var sut = new Record(dataRecord);

            var actual = sut.FieldCount;

            Assert.AreEqual(44, actual);
        }

        [TestMethod]
        public void GetBytes_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);
            var bytes = new byte[] { };
            dataRecord.GetBytes(Index, 2, bytes, 3, 4).Returns(5);

            var actual = sut.GetBytes(Index, 2, bytes, 3, 4);

            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void GetChars_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);
            var chars = new char[] { };
            dataRecord.GetChars(Index, 2, chars, 3, 4).Returns(5);

            var actual = sut.GetChars(Index, 2, chars, 3, 4);

            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void IsDBNull_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            dataRecord.IsDBNull(5).Returns(true);
            var sut = new Record(dataRecord);

            var actual = sut.IsDBNull(5);

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void GetValues_WithInt32AndDBNull_ReturnsInt32AndNull()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var values = new object[] { 5, DBNull.Value };
            dataRecord.GetValues(values).Returns(values.Length);
            var sut = new Record(dataRecord);

            var actual = sut.GetValues(values);

            Assert.AreEqual(values.Length, actual);
            Assert.AreEqual(5, values[0]);
            Assert.AreEqual(null, values[1]);
        }

        [TestMethod]
        public void GetNullable_Indexed_NullableGeneric_DBNull_ReturnsNull()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            dataRecord.IsDBNull(Index).Returns(true);
            var sut = new Record(dataRecord);

            var actual = sut.GetNullable<int>(Index);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetNullable_Indexed_NullableGeneric_Int32_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var expected = 123;
            dataRecord.GetValue(Index).Returns(expected);
            var sut = new Record(dataRecord);

            var actual = sut.GetNullable<int>(Index);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNullable_Named_NullableGeneric_DBNull_ReturnsNull()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            dataRecord.IsDBNull(Index).Returns(true);
            dataRecord.GetOrdinal(Name).Returns(Index);
            var sut = new Record(dataRecord);

            var actual = sut.GetNullable<int>(Name);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetNullable_Named_NullableGeneric_Int32_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var expected = 123;
            dataRecord.GetOrdinal(Name).Returns(Index);
            dataRecord.GetValue(Index).Returns(expected);
            var sut = new Record(dataRecord);

            var actual = sut.GetNullable<int>(Name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Indexed_Int32_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var expected = 123;
            dataRecord.GetValue(Index).Returns(expected);
            var sut = new Record(dataRecord);

            var actual = sut.Get<int>(Index);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Named_Int32_ReturnsExpectedValue()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var expected = 123;
            dataRecord.GetOrdinal(Name).Returns(Index);
            dataRecord.GetValue(Index).Returns(expected);
            var sut = new Record(dataRecord);

            var actual = sut.Get<int>(Name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Get_Class_ReturnsAnObjectWithExpectedValues()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            dataRecord.GetOrdinal("Id").Returns(0);
            dataRecord.GetOrdinal("Name").Returns(1);
            dataRecord.GetValue(0).Returns(123);
            dataRecord.GetValue(1).Returns("n");
            var sut = new Record(dataRecord);

            var actual = sut.Get<Product>();

            Assert.AreEqual(123, actual.Id);
            Assert.AreEqual("n", actual.Name);
        }

        [TestMethod]
        public void Get_Class_PropertyDoesNotExistInRecord_PropertyIsNull()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            dataRecord.GetOrdinal("Id").Returns(-1);
            var sut = new Record(dataRecord);

            var actual = sut.Get<Product>();

            Assert.AreEqual(null, actual.Id);
        }

        //[TestMethod]
        //public void Get_ClassWithNullClassMember_SetsPropertiesOfClassMember()
        //{
        //    var dataRecord = Substitute.For<IDataRecord>();
        //    dataRecord.GetOrdinal("CountryCode2").Returns(1);
        //    dataRecord.GetValue(1).Returns("FI");
        //    var sut = new Record(dataRecord);

        //    var actual = sut.Get<User>();

        //    Assert.AreEqual("FI", actual.Address.CountryCode2);
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_Null_ThrowsException()
        {
            var dataRecord = Substitute.For<IDataRecord>();
            var sut = new Record(dataRecord);

            sut.Get(null as User);
        }

        //[TestMethod]
        //public void Get_ClassWithNonNullClassMember_DoesNotChangeInstance()
        //{
        //    var dataRecord = Substitute.For<IDataRecord>();
        //    var sut = new Record(dataRecord);
        //    var customer = new Customer();
        //    var original = customer.Address;

        //    sut.Get(customer);

        //    Assert.AreSame(original, customer.Address);
        //}
    }
}
