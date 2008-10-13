using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SimpleCrud.Core {
    public class DataReaderDecorator :DbDataReader{

        private  DbDataReader dataReader;

        public void SetDataReader(DbDataReader dr) {
            dataReader = dr;
        }

        public DataReaderDecorator(DbDataReader dataReader):base() {
            this.dataReader = dataReader;
        }
        public DataReaderDecorator(DbCommand command)
            : this(command.ExecuteReader()) {            
        }

        public override void Close() {
            dataReader.Close();
        }

        public override int Depth {
            get {
                return dataReader.Depth;
            }
        }

        public override int FieldCount {
            get {
                return dataReader.FieldCount;
            }
        }

        public override bool GetBoolean(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(bool);
            return dataReader.GetBoolean(ordinal);
        }

        public  bool GetBoolean(String name) {
            return GetBoolean(GetOrdinal(name));
        }

        public override byte GetByte(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(byte);
            return dataReader.GetByte(ordinal);
        }
        public  byte GetByte(String name) {
            return GetByte(GetOrdinal(name));
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(long);
            return dataReader.GetBytes( ordinal,  dataOffset,  buffer,  bufferOffset,  length);
        }
        public long GetBytes(string name, long dataOffset, byte[] buffer, int bufferOffset, int length) {
            
            return GetBytes(this.GetOrdinal(name), dataOffset, buffer, bufferOffset, length);
        }

        public override char GetChar(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(char);

            return dataReader.GetChar(ordinal);
        }
        public  char GetChar(string name) {
            return GetChar(GetOrdinal(name));
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(long);
            return dataReader.GetChars( ordinal,  dataOffset,  buffer,  bufferOffset,  length);
        }

        public  long GetChars(string name, long dataOffset, char[] buffer, int bufferOffset, int length) {
            
            return GetChars(GetOrdinal(name), dataOffset, buffer, bufferOffset, length);
        }

        public override string GetDataTypeName(int ordinal) {
            return dataReader.GetDataTypeName(ordinal);
        }

        public override DateTime GetDateTime(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(DateTime);
            return dataReader.GetDateTime(ordinal);
        }

        public  DateTime GetDateTime(string name) {
            return GetDateTime(GetOrdinal(name));
        }

        public DateTime? GetNullableDateTime(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return null;
            return dataReader.GetDateTime(ordinal);
        }
        public DateTime? GetNullableDateTime(string name) {

            return GetNullableDateTime(GetOrdinal(name));
        }

        public override decimal GetDecimal(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(decimal);
            return dataReader.GetDecimal(ordinal);
        }

        public decimal GetDecimal(string name) {
            
            return GetDecimal(GetOrdinal(name));
        }

        public override double GetDouble(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(double);
            return dataReader.GetDouble(ordinal);
        }

        public double GetDouble(string name) {
            
            return GetDouble(GetOrdinal(name));
        }

        public override System.Collections.IEnumerator GetEnumerator() {
            return dataReader.GetEnumerator();
        }

        public override Type GetFieldType(int ordinal) {
            return dataReader.GetFieldType(ordinal);
        }

        public override float GetFloat(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(float);
            return GetFloat(ordinal);
        }
        public float GetFloat(string name) {
         
            return GetFloat(GetOrdinal(name));
        }

        public override Guid GetGuid(int ordinal) {
            return dataReader.GetGuid(ordinal);
        }

       

        public override short GetInt16(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(short);
            return dataReader.GetInt16(ordinal);
        }

        public override int GetInt32(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(int);
            return dataReader.GetInt32(ordinal);
        }
        public int GetInt32(string name) {
            
            return GetInt32(GetOrdinal(name));
        }

        public override long GetInt64(int ordinal) {
            if (dataReader.GetValue(ordinal) == DBNull.Value)
                return default(long);
            return dataReader.GetInt64(ordinal);
        }

        public  long GetInt64(string name) {
         
            return GetInt64(GetOrdinal(name));
        }

        public  override string GetName(int ordinal) {
            return dataReader.GetName(ordinal);
        }

        public override int GetOrdinal(string name) {
            return dataReader.GetOrdinal(name);
        }

        public override System.Data.DataTable GetSchemaTable() {
            return dataReader.GetSchemaTable();
        }

        public override string GetString(int ordinal) {
            if (GetValue(ordinal) == DBNull.Value)
                return null;
            return dataReader.GetString(ordinal);
        }
        public  string GetString(string name) {
         
            return dataReader.GetString(GetOrdinal(name));
        }

        public override object GetValue(int ordinal) {
            return dataReader.GetValue(ordinal);
        }

        public override int GetValues(object[] values) {
            return dataReader.GetValues(values);
        }

        public override bool HasRows {
            get {
                return dataReader.HasRows;
            }
        }

        public override bool IsClosed {
            get {
                return dataReader.IsClosed;
            }
        }

        public override bool IsDBNull(int ordinal) {
            return dataReader.IsDBNull(ordinal);
        }

        public override bool NextResult() {
            return dataReader.NextResult();
        }

        public override bool Read() {
            return dataReader.Read();
        }

        public override int RecordsAffected {
            get {
                return dataReader.RecordsAffected;
            }
        }

        public override object this[string name] {
            get {
                return dataReader[name];
            }
        }

        public override object this[int ordinal] {
            get {
                return dataReader[ordinal];
            }
        }

        public IList<String> GetColumnsNames() {
            IList<String> list =  new List<String>();
            try {
                for (int i = 0; i < this.FieldCount; i++) {
                    list.Add(this.GetName(i));
                }
            } catch (Exception ex) {
                throw ex;
            }
            return list;
        }
    }
}
