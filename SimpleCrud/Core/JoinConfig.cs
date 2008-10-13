using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCrud.Core {
    public class JoinConfig {

        private readonly Type type;

        private readonly String tableName;
        private readonly IList<DBField> foreginKeys;
        private readonly IDictionary<string ,string > joinDic;
        private String propertyName;

        public string PropertyName {
            get { return propertyName; }
            
        }

        public JoinConfig(Type type, String tableName, String propertyName, string foreignColumn, string pkColumn ) {

            this.type = type;
            this.tableName = tableName;
            joinDic = new Dictionary<string, string>();
            this.propertyName = propertyName;
            joinDic.Add(foreignColumn,pkColumn);
        }
        public void addJoinField( string foreignField, string pkField) {
            joinDic.Add(foreignField, pkField);
        }
        /// <summary>
        /// Retorna um  Enumerator contendo os nomes das colunas 
        /// que fazem parte do join
        /// </summary>
        /// <returns><pre>IEnumerator< KeyValuePair<string, string>> </pre> </returns>
        public IEnumerator< KeyValuePair<string, string>> GetJoins() {
            return joinDic.GetEnumerator();
        }
        /// <summary>
        /// Retorna o Simple Config do Join
        /// </summary>
        /// <returns></returns>
        public SimpleConfig GetJoinedConfig() {
            return ConfigManager.GetInstance().GetConfig(type);
        }

        public Type Type {
            get {
                return type;
            }
            
        }

        public String getTableName() {

            return tableName;
        }

        public String toString() {

            StringBuilder sb = new StringBuilder(512);

            sb.Append("JoinConfig: ").Append("beanClass=").Append(type.Name);

            sb.Append(" tableName=").Append(tableName);

            return sb.ToString();
        }
    }
}
