using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Mutex.Data
{
    /// <summary>
    /// Represents a Record class.
    /// </summary>
    public class Record : IRecord
    {
        /// <summary>
        /// The underlying IDataRecord object.
        /// </summary>
        protected readonly IDataRecord DataRecord;

        /// <summary>
        /// Initializes a new instance of the Record class.
        /// </summary>
        /// <param name="dataRecord">An IDataRecord object which provides the data.</param>
        /// <exception cref="ArgumentNullException">The dataRecord was null.</exception>
        public Record(IDataRecord dataRecord)
        {
            this.DataRecord = dataRecord ?? throw new ArgumentNullException(nameof(dataRecord));
        }

        /// <summary>
        /// Gets the column located at the specified index.
        /// </summary>
        /// <param name="i">The zero-based index of the column to get.</param>
        /// <returns>The column located at the specified index as an System.Object.</returns>
        public object this[int i]
        {
            get
            {
                if (this.IsDBNull(i))
                {
                    return null;
                }
                return this.DataRecord[i];
            }
        }

        /// <summary>
        /// Gets the column with the specified name.
        /// </summary>
        /// <param name="name">The name of the column to find.</param>
        /// <returns>The column with the specified name as an System.Object.</returns>
        public object this[string name]
        {
            get
            {
                var value = this.DataRecord[name];
                if (value == DBNull.Value)
                {
                    return null;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        public int FieldCount => this.DataRecord.FieldCount;

        /// <summary>
        /// Get bool value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public bool GetBoolean(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetBoolean(i);
        }

        /// <summary>
        /// Get bool value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public bool GetBoolean(int i)
        {
            return this.DataRecord.GetBoolean(i);
        }

        /// <summary>
        /// Get byte value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public byte GetByte(int i)
        {
            return this.DataRecord.GetByte(i);
        }

        /// <summary>
		/// Reads a stream of bytes from the specified column.
		/// </summary>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column, or null, if column's data was null.</returns>
		public byte[] GetBytes(int ordinal)
        {
            if (this.IsDBNull(ordinal))
            {
                return null;
            }

            long size = this.GetBytes(ordinal, 0, null, 0, 0);
            if (size < int.MaxValue)
            {
                byte[] bytes = new byte[size];
                long read = this.GetBytes(ordinal, 0, bytes, 0, (int)size);
                if (read != size)
                {
                    var message = string.Format("Strange thing occurred. The first call to GetBytes, to determine the required size, returned {0} bytes and the second call, to read the bytes, returned {1} bytes.",
                        /*0*/size,
                        /*1*/read);
                    throw new DataException(message);
                }
                return bytes;
            }
            else
            {
                // the data in the column is > 2GB
                throw new NotSupportedException("GetBytes method does not support reading data over 2GB.");
            }
        }

        /// <summary>
		/// Reads a stream of bytes from the specified column.
		/// </summary>
		/// <param name="name">The name of the column.</param>
		/// <returns>The value of the specified column.</returns>
		public byte[] GetBytes(string name)
        {
            int ordinal = this.GetOrdinal(name);
            return this.GetBytes(ordinal);
        }

        /// <summary>
        /// Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <param name="fieldOffset">The index within the field from which to start the read operation.</param>
        /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
        /// <param name="bufferoffset">The index for buffer to start the read operation.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return this.DataRecord.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// Get char value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public char GetChar(int i)
        {
            return this.DataRecord.GetChar(1);
        }

        /// <summary>
        /// Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given buffer offset.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <param name="fieldoffset">The index within the row from which to start the read operation.</param>
        /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
        /// <param name="bufferoffset">The index for buffer to start the read operation.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The actual number of characters read.</returns>
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return this.DataRecord.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// Returns an System.Data.IDataReader for the specified column ordinal.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>The System.Data.IDataReader for the specified column ordinal.</returns>
        public IDataReader GetData(int i)
        {
            return this.DataRecord.GetData(i);
        }

        /// <summary>
        /// Gets the data type information for the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>The data type information for the specified field.</returns>
        public string GetDataTypeName(int i)
        {
            return this.DataRecord.GetDataTypeName(i);
        }

        /// <summary>
        /// Get DateTime value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public DateTime GetDateTime(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetDateTime(i);
        }

        /// <summary>
        /// Get DateTime value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public DateTime GetDateTime(int i)
        {
            return this.DataRecord.GetDateTime(i);
        }

        /// <summary>
        /// Get decimal value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public decimal GetDecimal(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetDecimal(i);
        }

        /// <summary>
        /// Get decimal value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public decimal GetDecimal(int i)
        {
            return this.DataRecord.GetDecimal(i);
        }

        /// <summary>
        /// Get double value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public double GetDouble(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetDouble(i);
        }

        /// <summary>
        /// Get double value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public double GetDouble(int i)
        {
            return this.DataRecord.GetDouble(i);
        }

        /// <summary>
        /// Gets the System.Type information corresponding to the type of System.Object that would be returned from System.Data.IDataRecord.GetValue(System.Int32).
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>The System.Type information corresponding to the type of System.Object that would be returned from System.Data.IDataRecord.GetValue(System.Int32).</returns>
        public Type GetFieldType(int i)
        {
            return this.DataRecord.GetFieldType(i);
        }

        /// <summary>
        /// Get float value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public float GetFloat(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetFloat(i);
        }

        /// <summary>
        /// Get float value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public float GetFloat(int i)
        {
            return this.DataRecord.GetFloat(i);
        }

        /// <summary>
        /// Get Guid value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public Guid GetGuid(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetGuid(i);
        }

        /// <summary>
        /// Get Guid value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public Guid GetGuid(int i)
        {
            return this.DataRecord.GetGuid(i);
        }

        /// <summary>
        /// Get Int16 value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public short GetInt16(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetInt16(i);
        }

        /// <summary>
        /// Get Int16 value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public short GetInt16(int i)
        {
            return this.DataRecord.GetInt16(i);
        }

        /// <summary>
        /// Get Int32 value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public int GetInt32(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetInt32(i);
        }

        /// <summary>
        /// Get Int32 value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public int GetInt32(int i)
        {
            return this.DataRecord.GetInt32(i);
        }

        /// <summary>
        /// Get Int64 value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public long GetInt64(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetInt64(i);
        }

        /// <summary>
        /// Get Int64 value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public long GetInt64(int i)
        {
            return this.DataRecord.GetInt64(i);
        }

        /// <summary>
        /// Gets the name for the field to find.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>The name of the field or the empty string (""), if there is no value to return.</returns>
        public string GetName(int i)
        {
            return this.DataRecord.GetName(i);
        }

        /// <summary>
        /// Get nullable bool value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value or null.</returns>
        public bool? GetNullableBoolean(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetNullableBoolean(i);
        }

        /// <summary>
        /// Get nullable bool value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value or null.</returns>
        public bool? GetNullableBoolean(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.GetBoolean(i);
        }

        /// <summary>
        /// Get nullable DateTime value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value or null.</returns>
        public DateTime? GetNullableDateTime(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetNullableDateTime(i);
        }

        /// <summary>
        /// Get nullable DateTime value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value or null.</returns>
        public DateTime? GetNullableDateTime(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.GetDateTime(i);
        }

        /// <summary>
        /// Get nullable decimal value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value or null.</returns>
        public decimal? GetNullableDecimal(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetNullableDecimal(i);
        }

        /// <summary>
        /// Get nullable decimal value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value or null.</returns>
        public decimal? GetNullableDecimal(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.GetDecimal(i);
        }

        /// <summary>
        /// Get nullable double value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value or null.</returns>
        public double? GetNullableDouble(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetNullableDouble(i);
        }

        /// <summary>
        /// Get nullable double value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value or null.</returns>
        public double? GetNullableDouble(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.GetDouble(i);
        }

        /// <summary>
        /// Get nullable float value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value or null.</returns>
        public float? GetNullableFloat(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetNullableFloat(i);
        }

        /// <summary>
        /// Get nullable float value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value or null.</returns>
        public float? GetNullableFloat(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.GetFloat(i);
        }

        /// <summary>
        /// Get nullable Guid value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value or null.</returns>
        public Guid? GetNullableGuid(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetNullableGuid(i);
        }

        /// <summary>
        /// Get nullable Guid value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value or null.</returns>
        public Guid? GetNullableGuid(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.GetGuid(i);
        }

        /// <summary>
        /// Get nullable Int16 value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value or null.</returns>
        public short? GetNullableInt16(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetNullableInt16(i);
        }

        /// <summary>
        /// Get nullable Int16 value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value or null.</returns>
        public short? GetNullableInt16(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.GetInt16(i);
        }

        /// <summary>
        /// Get nullable Int32 value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value or null.</returns>
        public int? GetNullableInt32(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetNullableInt32(i);
        }

        /// <summary>
        /// Get nullable Int32 value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value or null.</returns>
        public int? GetNullableInt32(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.GetInt32(i);
        }

        /// <summary>
        /// Get nullable Int64 value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value or null.</returns>
        public long? GetNullableInt64(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetNullableInt64(i);
        }

        /// <summary>
        /// Get nullable Int64 value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value or null.</returns>
        public long? GetNullableInt64(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.GetInt64(i);
        }

        /// <summary>
        /// Return the index of the named field.
        /// </summary>
        /// <param name="name">The name of the field to find.</param>
        /// <returns>The index of the named field.</returns>
        public int GetOrdinal(string name)
        {
            return this.DataRecord.GetOrdinal(name);
        }

        /// <summary>
        /// Get string value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public string GetString(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetString(i);
        }

        /// <summary>
        /// Get string value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public string GetString(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.DataRecord.GetString(i);
        }

        /// <summary>
        /// Get value by field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value.</returns>
        public object GetValue(string name)
        {
            var i = this.GetOrdinal(name);
            return this.GetValue(i);
        }

        /// <summary>
        /// Get value by field index.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>The value.</returns>
        public object GetValue(int i)
        {
            if (this.IsDBNull(i))
            {
                return null;
            }
            return this.DataRecord.GetValue(i);
        }

        /// <summary>
        /// Populates an array of objects with the column values of the current record.
        /// </summary>
        /// <param name="values">An array of System.Object to copy the attribute fields into.</param>
        /// <returns>The number of instances of System.Object in the array.</returns>
        public int GetValues(object[] values)
        {
            var length = this.DataRecord.GetValues(values);
            if (values != null)
            {
                for (var i = 0; i < values.Length; ++i)
                {
                    if (values[i] == DBNull.Value)
                    {
                        values[i] = null;
                    }
                }
            }
            return length;
        }

        /// <summary>
        /// Return whether the specified field is set to null.
        /// </summary>
        /// <param name="i">The index of the field.</param>
        /// <returns>true if the specified field is set to null; otherwise, false.</returns>
        public bool IsDBNull(int i)
        {
            return this.DataRecord.IsDBNull(i);
        }

        /// <summary>
        /// Gets the value of the specified field as nullable type.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field or null.</returns>
        public T? GetNullable<T>(int i)
            where T : struct
        {
            var value = this.GetValue(i);
            if (value == null)
            {
                return null;
            }

            return ConvertHelper.ChangeType<T>(value);
        }

        /// <summary>
        /// Gets the value of the specified field as nullable type.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field or null.</returns>
        public T? GetNullable<T>(string name)
            where T : struct
        {
            var i = this.GetOrdinal(name);
            return this.GetNullable<T>(i);
        }

        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="i">The zero-based field ordinal.</param>
        /// <returns>The value of the field.</returns>
        public T Get<T>(int i)
        {
            var value = this.GetValue(i);
            return ConvertHelper.ChangeType<T>(value);
        }

        /// <summary>
        /// Gets the value of the named field.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        public T Get<T>(string name)
        {
            var i = this.GetOrdinal(name);
            return this.Get<T>(i);
        }

        /// <summary>
        /// Gets an object of type T by creating an instance of type T, setting the values of properties with values from the matching fields in the record.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <returns>A new instance of type T.</returns>
        /// <remarks>
        /// The type T must have a parameterless constructor.
        /// </remarks>
        public T Get<T>()
        {
            var obj = ObjectFactory.CreateInstance<T>();
            this.GetInternal(obj);
            return obj;
        }

        /// <summary>
        /// Get the values of matching properties and fields in the record.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object which is the target.</param>
        /// <exception cref="ArgumentNullException">The obj was null.</exception>
        public void Get<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            this.GetInternal(obj);
        }

        void GetInternal(object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                this.TryGetValueFromRecordAndTrySetValueOfMatchingProperty(property, obj);
            }
        }

        void TryGetValueFromRecordAndTrySetValueOfMatchingProperty(PropertyInfo property, object obj)
        {
            var ordinal = this.GetOrdinal(property.Name);
            if (ordinal >= 0)
            {
                var value = this.GetValue(ordinal);
                this.TrySetPropertyValue(property, obj, value);
            }
        }

        void TrySetPropertyValue(PropertyInfo property, object obj, object value)
        {
            var setMethod = this.GetSetMethod(property);
            if (setMethod != null)
            {
                this.SetPropertyValue(setMethod, obj, value);
            }
        }

        MethodInfo GetSetMethod(PropertyInfo property)
        {
            return property.GetSetMethod(nonPublic: true); // TODO config
        }

        void SetPropertyValue(MethodInfo setMethod, object obj, object value)
        {
            setMethod.Invoke(obj, new object[] { value });
        }
    }
}
