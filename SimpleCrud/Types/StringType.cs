using System;
using System.Data.Common;
using SimpleCrud.Core;


namespace SimpleCrud.Types {
    public class StringType : IDBType {

       

       public void BindToCommand(DbCommand cmd, int index, object value) {
           if (value == null) {
               cmd.Parameters[index].Value = DBNull.Value;
           } else {
               cmd.Parameters[index].Value = value.ToString().Trim();
           }
       }

       public void BindToCommand(DbCommand cmd, string paramName, object value) {
           if (value == null) {
               cmd.Parameters[paramName].Value = DBNull.Value;
           } else {
               cmd.Parameters[paramName].Value = value.ToString().Trim();
           }
       }

       public object GetFromDataReader(DbDataReader rset, int index) {

           return rset.GetValue(index) == DBNull.Value ? null : rset.GetString(index).Trim();
       }

       public object GetFromDataReader(DbDataReader rset, string fieldName) {
           return rset[fieldName] == DBNull.Value ? null : rset[fieldName].ToString().Trim();
       }

       public Type GetImplType() {
           return typeof(String);
       }

       
   }
}
