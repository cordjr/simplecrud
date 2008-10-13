using System;
using System.Data.Common;


namespace SimpleCrud.Core {
     public interface IDBType {
         void BindToCommand(DbCommand cmd, int index, Object value);
         void BindToCommand(DbCommand cmd, String paramName, Object value);
         Object GetFromDataReader(DbDataReader rset, int index);
         Object GetFromDataReader(DbDataReader rset, string fieldName);
         Type GetImplType();

    }
}
