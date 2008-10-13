using System;
using SimpleCrud.Core;

namespace SimpleCrud.Types {
    public class AutoIncrementType: IDBType {
        public void BindToCommand(System.Data.Common.DbCommand cmd, int index, object value) {
            if (value == null) {
                cmd.Parameters[index].Value = DBNull.Value;
            } else {
                cmd.Parameters[index].Value = (int)value;
            }
        }

        public void BindToCommand(System.Data.Common.DbCommand cmd, string paramName, object value) {
            if (value == null) {
                cmd.Parameters[paramName].Value = DBNull.Value;
            } else {
                cmd.Parameters[paramName].Value = (int)value;
            }
        }

        public object GetFromDataReader(System.Data.Common.DbDataReader rset, int index) {
            return rset.GetValue(index) == DBNull.Value ? 0 : rset.GetValue(index);
        }

        public Type GetImplType() {
            return typeof(int);
        }


        public object GetFromDataReader(System.Data.Common.DbDataReader rset, string fieldName) {
            return rset[fieldName] == DBNull.Value ? 0 : rset[fieldName];
        }

    }
}
