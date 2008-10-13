using System;
using System.Data.Common;
using SimpleCrud.Core;


namespace SimpleCrud.Types {
    public class LongType : IDBType {

        

        public void BindToCommand(DbCommand cmd, int index, object value) {
            if (value == null) {
                cmd.Parameters[index].Value = DBNull.Value;
            } else {
                cmd.Parameters[index].Value = (long)value;
            }
        }

        public void BindToCommand(DbCommand cmd, string paramName, object value) {
            if (value == null) {
                cmd.Parameters[paramName].Value = DBNull.Value;
            } else {
                cmd.Parameters[paramName].Value = (long)value;
            }
        }

        public object GetFromDataReader(DbDataReader rset, int index) {
            return rset.GetValue(index) == DBNull.Value ? 0 : rset.GetValue(index);
        }

        public object GetFromDataReader(DbDataReader rset, string fieldName) {
            return rset[fieldName] == DBNull.Value ? 0 : rset[fieldName];
        }

        public Type GetImplType() {
            return typeof (long);
        }

        
    }
}
