using System;
using System.Data.Common;
using SimpleCrud.Core;

namespace SimpleCrud.Types {

    public class DoubleType : IDBType {
        public void BindToCommand(DbCommand cmd, int index, object value) {
            if (value == null) {
                cmd.Parameters[index].Value = null;
            } else {
                cmd.Parameters[index].Value = ((Double)value);
            }
        }

        public void BindToCommand(DbCommand cmd, string paramName, object value) {
            if (value == null) {
                cmd.Parameters[paramName].Value = null;
            } else {
                cmd.Parameters[paramName].Value = ((Double)value);
            }
        }

        public object GetFromDataReader(DbDataReader rset, int index) {
            return rset.GetValue(index) == DBNull.Value ? 0.0 : rset.GetValue(index);
        }

        public object GetFromDataReader(DbDataReader rset, string fieldName) {
            return rset[fieldName] == DBNull.Value ? 0 : rset[fieldName];
        }

        public Type GetImplType() {
            return typeof(Double);
        }
    }
}
