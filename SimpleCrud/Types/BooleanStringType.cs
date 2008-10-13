using System;
using System.Data.Common;
using SimpleCrud.Core;

namespace SimpleCrud.Types {
    public class BooleanStringType : IDBType {
        private const String STrue = "T";
        private const String sFalse = "F";
        private static Boolean getBoolValue(String x) {

            if (x.Equals("T",StringComparison.InvariantCultureIgnoreCase))
                return true;
            else if (x.Equals("F",StringComparison.InvariantCultureIgnoreCase))
                return false;
            else if (x.Equals("true",StringComparison.InvariantCultureIgnoreCase))
                return true;
            else if (x.Equals( "false",StringComparison.InvariantCultureIgnoreCase))
                return false;
            else if (x.Equals("0",StringComparison.InvariantCultureIgnoreCase))
                return false;
            else if (x.Equals( "true",StringComparison.InvariantCultureIgnoreCase))
                return true;
            throw new Exception("String is not T or F: " + x);
        }
        public void BindToCommand(DbCommand cmd, int index, object value) {
            if (value != null) {
                bool b = getBoolValue(((string) value));
                cmd.Parameters[index].Value = b ? STrue : sFalse;
            }
            else {
                cmd.Parameters[index].Value = DBNull.Value;
            }
        }

        public void BindToCommand(DbCommand cmd, string paramName, object value) {
            if (value != null) {
                bool b = getBoolValue(((string) value));
                cmd.Parameters[paramName].Value = b ? STrue : sFalse;
            }
            else {
                cmd.Parameters[paramName].Value = DBNull.Value;
            }
        }

        public object GetFromDataReader(DbDataReader rset, int index) {
            return rset.GetValue(index) == DBNull.Value ? false : getBoolValue((String) rset.GetValue(index));
            
        }

        public object GetFromDataReader(DbDataReader rset, string fieldName) {
            return rset[fieldName] == DBNull.Value ? false : getBoolValue((String) rset[fieldName]);
        }

        public Type GetImplType() {
            return typeof (bool);
        }
    }
}
