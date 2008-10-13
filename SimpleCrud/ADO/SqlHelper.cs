using System;
using System.Collections.Generic;
using System.Text;
using SimpleCrud.Core;
using SimpleCrud.Types;

namespace SimpleCrud.ADO {
    class SqlHelper {
        private static string FieldsToStringForInsert(IList<DBField> fieldList, bool isParams) {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            string param = isParams ? "@" : "";

            foreach (DBField field in fieldList) {
                if (count > 0 && !(field.Type is AutoIncrementType)) {
                    sb.Append(", ");

                    sb.AppendLine(param + field.getDbName() + " ");
                    count++;

                } else if (count == 0 && !(field.Type is AutoIncrementType)) {
                    sb.AppendLine(param + field.getDbName() + " ");
                    count++;
                }


            }
            return sb.ToString();

        }
        private static string FieldsToStringForSelect(IList<DBField> fieldList) {
            StringBuilder sb = new StringBuilder();
            int count = 0;

            foreach (DBField field in fieldList) {
                if (count > 0) {
                    sb.Append(", ");
                }
                sb.AppendLine(field.getDbName() + " ");
                count++;
            }
            return sb.ToString();

        }

        private static string FieldsToStringForUpdate(IList<DBField> fieldList) {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (DBField field in fieldList) {
                if (count > 0)
                    sb.Append(", ");

                sb.AppendLine(field.getDbName() + " = @" + field.getDbName());
                count++;

            }
            return sb.ToString();
        }

        public static string CreateInsert(SimpleConfig config) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO " + config.TableName);

            sb.AppendLine(" ( ");
            IList<DBField> fieldList = config.Fields;


            bool hasAutoIncrementPk = config.HasAutoIncrementPK();

            sb.Append(FieldsToStringForInsert(fieldList, false));
            sb.AppendLine(" ) VALUES ( ");

            if (!config.hasPK()) {
                throw new SimpleBeanException(config.Type.Name + " dont has a Pk Configured");
            }






            sb.Append(FieldsToStringForInsert(fieldList, true));

            sb.AppendLine(")");

            if (hasAutoIncrementPk) {
                sb.Append("  SET @GeneratedID = SCOPE_IDENTITY()");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Create  a SQL Update Statement 
        /// 
        /// </summary>
        /// <param name="config">SimpleConfig  used to generate a SQL String</param>
        /// <returns></returns>
        public static string CreateUpdate(SimpleConfig config) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE " + config.TableName);

            sb.AppendLine(" SET ");
            IList<DBField> fieldList = config.Fields;

            int count = 0;

            sb.Append(FieldsToStringForUpdate(fieldList));

            sb.AppendLine(" WHERE     ");
            if (!config.hasPK())
                throw new SimpleBeanException(config.Type.Name + " dont has a Pk Configured");

            IList<DBField> pkList = config.PKS;

            count = 0;


            foreach (DBField pk in pkList) {
                if (count > 0)
                    sb.Append(" AND ");
                sb.Append(pk.getDbName()).Append(" = ").Append("@" + pk.getDbName());
                count++;
            }

            return sb.ToString();
        }
        /// <summary>
        /// Create a Select Statement 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string CreateSelect(SimpleConfig config) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT ");
            IList<DBField> fieldList = config.Fields;

            sb.Append(FieldsToStringForSelect(fieldList));
            sb.AppendLine(" FROM " + config.TableName);
            return sb.ToString();
        }

        /// <summary>
        /// Create a Select Statement 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="uniqueResult"></param>
        /// <returns></returns>
        public static string CreateSelect(SimpleConfig config, bool uniqueResult) {
            StringBuilder sb = new StringBuilder();


            if (uniqueResult) {
                return CreateSelect(config);
            } else {
                sb.Append(CreateSelect(config));
            }
            sb.AppendLine(" WHERE     ");
            int count = 0;
            foreach (DBField pk in config.PKS) {
                if (count > 0)
                    sb.Append(" AND ");
                sb.Append(pk.getDbName()).Append(" = ").Append("@" + pk.getDbName());
                count++;
            }
            return sb.ToString();
        }

        public static string CreateSelectWithFullJoin(SimpleConfig config) {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT ");
            IList<DBField> fieldList = config.Fields;

            int count = 0;
            foreach (DBField field in fieldList) {
                if (count > 0)
                    sb.Append(", ");
                sb.AppendLine(config.TableName + "." + field.getDbName());
                count++;
            }
            IList<JoinConfig> listJoin = config.GetJoinConfigList();
            foreach (JoinConfig jConf in listJoin) {
                foreach (DBField field in jConf.GetJoinedConfig().Fields)
                    sb.Append(", " + jConf.getTableName() + "." + field.getDbName() + " as "
                              + jConf.getTableName() + "_" + field.getDbName());

            }

            sb.AppendLine(" FROM " + config.TableName + "   ");
            foreach (JoinConfig jConf in listJoin) {
                sb.AppendLine("Left JOIN " + jConf.getTableName() + " on ");
                IEnumerator<KeyValuePair<string, string>> enJoins = jConf.GetJoins();
                count = 0;
                while (enJoins.MoveNext()) {
                    KeyValuePair<string, string> keyValue = enJoins.Current;
                    if (count > 0)
                        sb.AppendLine(" AND ");
                    sb.AppendLine(jConf.getTableName() + "." + keyValue.Key + " = " + config.TableName + "." +
                                  keyValue.Value);
                    count++;
                }
            }
            return sb.ToString();
        }



        public static string CreateSelectWithFullJoin(SimpleConfig config, bool uniqueResult) {



            if (uniqueResult) {
                return CreateSelectWithFullJoin(config);
            } else {
                IList<DBField> pkList = config.PKS;
                StringBuilder sb = new StringBuilder();
                sb.Append(CreateSelectWithFullJoin(config));
                int count = 0;


                foreach (DBField pk in pkList) {
                    if (count > 0)
                        sb.Append(" AND ");
                    sb.Append(config.TableName + "." + pk.getDbName()).Append(" = ").Append("@" + pk.getDbName());
                    count++;
                }

                return sb.ToString();
            }

        }
    }
}
