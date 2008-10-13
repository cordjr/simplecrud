using System;
using System.Data.Common;
using SimpleCrud.Core;


namespace SimpleCrud.Types {
    public class BooleanIntType : IDBType {
        public String toString() {
            return GetType().Name;
        }

        private static Boolean getBoolValue(int x) {

            if (x == 1)
                return true;

            if (x == 0)
                return false;

            throw new Exception("integer is not 0 or 1: " + x);
        }

        public void BindToCommand(DbCommand cmd, int index, object value) {
            if (value != null) {
                cmd.Parameters[index].Value = DBNull.Value;
            } else {
                cmd.Parameters[index].Value = ((bool) value) ? 1 : 0;
            }
             
        }

        public void BindToCommand(DbCommand cmd, string paramName, object value) {
            if (value != null) {
                cmd.Parameters[paramName].Value = DBNull.Value;
            } else {
                cmd.Parameters[paramName].Value = ((bool)value) ? 1 : 0;
            }
        }


        
        public object GetFromDataReader(DbDataReader rset, int index) {
            return rset.GetValue(index) == DBNull.Value ? false : getBoolValue((int) rset.GetValue(index));

        }

        public object GetFromDataReader(DbDataReader rset, string fieldName) {
            return rset[fieldName] == DBNull.Value ? false : getBoolValue((int) rset[fieldName]);
        }

        public Type GetImplType() {
            return typeof(bool);
        }


    }
}
