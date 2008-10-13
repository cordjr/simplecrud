using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using SimpleCrud.Core;

using SimpleCrud.Types;
using SimpleCrud.Utils;


namespace SimpleCrud.ADO {
    public class SQLServerSession : ISimpleSession {
        private readonly DbConnection connectcion;
        private readonly DbCommand cmdInsert;
        protected virtual string GetStrParam(String paramName) {
            return "@" + paramName;
        }


        public SQLServerSession(DbConnection connectcion) {
            this.connectcion = connectcion;
        }

        #region ISimpleSession Members

        public void GetWithFulljoin(Object obj) {
            //Get(obj)
            SimpleConfig config = ConfigManager.GetInstance().
                GetConfig(obj.GetType());
            if (config == null)
                throw new SimpleBeanException(obj.GetType().Name + " dont has a SimpleConfig");
            if (config.getNumberOfFields() == 0)
                throw new SimpleBeanException("SimpleConfig dont have Fields");
            StringBuilder sb = new StringBuilder();

            DbCommand cmd = connectcion.CreateCommand();
            cmd.CommandText = SqlHelper.CreateSelectWithFullJoin(config, true);

            foreach (DBField pk in config.PKS) {
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = GetStrParam(pk.getDbName());
                param.Value = ReflectionUtils.GetPropertyValue(obj, pk.getName());
                cmd.Parameters.Add(param);
            }
            FillWithFullJoin(obj, cmd, config);
        }

        private void FillWithFullJoin(object obj, DbCommand cmd, SimpleConfig config) {

            DbDataReader dr = cmd.ExecuteReader();
            IDictionary<string, object> dic = new Dictionary<string, object>();
            IList<DBField> pkList = config.PKS;
            IList<DBField> fieldList = config.Fields;
            IList<JoinConfig> listJoin = config.GetJoinConfigList();
            try {
                #region Cria e Preenche os objetos
                while (dr.Read()) {
                    String hash = "";
                    foreach (DBField pk in pkList) {
                        hash += dr[pk.getDbName()].ToString();
                    }
                    if (!dic.ContainsKey(hash)) {
                        foreach (DBField field in fieldList) {
                            object value = field.Type.GetFromDataReader(dr, field.getName());
                            ReflectionUtils.SetPropertyValue(obj, field.getName(), value);
                        }

                        dic.Add(hash, obj);
                    }
                    foreach (JoinConfig jConf in listJoin) {
                        SimpleConfig cfg = jConf.GetJoinedConfig();
                        Type propType = ReflectionUtils.GetPropertyType(obj, jConf.PropertyName);

                        object joinObj;
                        if (ReflectionUtils.IsCollection(propType)) {

                            if (ReflectionUtils.GetPropertyValue(obj, jConf.PropertyName) == null) {
                                if (propType.IsGenericType) {
                                    joinObj =
                                        typeof(List<>).MakeGenericType(cfg.Type).GetConstructor(Type.EmptyTypes).Invoke
                                            (null);

                                } else {
                                    joinObj = typeof(ArrayList).GetConstructor(Type.EmptyTypes).Invoke(null);
                                    propType.GetType().MakeGenericType(new Type[] { cfg.Type }).GetConstructor(
                                        Type.EmptyTypes).Invoke(null);
                                }

                                ReflectionUtils.SetPropertyValue(obj, jConf.PropertyName, joinObj);
                            } else {
                                joinObj = ReflectionUtils.GetPropertyValue(obj, jConf.PropertyName);
                            }

                            object listItem = cfg.Type.GetConstructor(Type.EmptyTypes).Invoke(null);
                            foreach (DBField field in cfg.Fields) {
                                object val = field.Type.GetFromDataReader(dr,
                                                                                 cfg.TableName + "_" +
                                                                                 field.getDbName());

                                ReflectionUtils.SetPropertyValue(listItem, field.getName(), val);

                            }
                            if (!ReflectionUtils.FindItemInList(ReflectionUtils.GetPropertyInfo(obj, jConf.PropertyName), obj, cfg, listItem)) {
                                ReflectionUtils.CallMethod(joinObj, "Add", new object[] { listItem });
                            }

                        } else {
                            if (ReflectionUtils.GetPropertyValue(obj, jConf.PropertyName) == null) {
                                object joinedObj = cfg.Type.GetConstructor(Type.EmptyTypes).Invoke(null);
                                foreach (DBField field in cfg.Fields) {
                                    object value = field.Type.GetFromDataReader(dr,
                                                                                     cfg.TableName + "_" +
                                                                                     field.getDbName());

                                    ReflectionUtils.SetPropertyValue(joinedObj, field.getName(), value);
                                }
                            }

                        }


                    }

                }
                #endregion
            } finally {
                dr.Close();
            }
          
        }

        public bool Get(object obj) {
            SimpleConfig config = ConfigManager.GetInstance().
                GetConfig(obj.GetType());
            if (config == null)
                throw new SimpleBeanException(obj.GetType().Name + " dont has a SimpleConfig");
            if (config.getNumberOfFields() == 0)
                throw new SimpleBeanException("SimpleConfig dont have Fields");

            if (!config.hasPK())
                throw new SimpleBeanException(obj.GetType().Name + " dont has a Pk Configured");

            DbCommand cmd = connectcion.CreateCommand();
            cmd.CommandText = SqlHelper.CreateSelect(config, true);

            foreach (DBField pk in config.PKS) {
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = GetStrParam(pk.getDbName());
                param.Value = ReflectionUtils.GetPropertyValue(obj, pk.getName());
                cmd.Parameters.Add(param);
            }
            DbDataReader dr = cmd.ExecuteReader();
            try {
               
                IDictionary<string, object> dic = new Dictionary<string, object>();
                if (dr.Read()) {
                    foreach (DBField field in config.Fields) {
                        object value = field.Type.GetFromDataReader(dr, field.getName());
                        ReflectionUtils.SetPropertyValue(obj, field.getName(), value);

                    }
                    return true;
                } else {
                    return false;
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                dr.Close();
            }








        }

        public int Update(object obj) {
            SimpleConfig config = ConfigManager.GetInstance().
                GetConfig(obj.GetType());
            if (config == null)
                throw new SimpleBeanException(obj.GetType().Name + " dont has a SimpleConfig");
            if (config.getNumberOfFields() == 0)
                throw new SimpleBeanException("SimpleConfig dont have Fields");


            DbCommand cmd = connectcion.CreateCommand();

            cmd.CommandText = SqlHelper.CreateUpdate(config);



            foreach (DBField field in config.Fields) {
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = GetStrParam(field.getDbName());
                param.Value = ReflectionUtils.GetPropertyValue(obj, field.getName());
                cmd.Parameters.Add(param);
            }

            try {
                return cmd.ExecuteNonQuery();
            } catch (Exception ex) {
                throw ex;
            }


        }

        public int Update(object obj, bool dynaUpdate) {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Insert(object obj) {
            SimpleConfig config = ConfigManager.GetInstance().
                GetConfig(obj.GetType());
            if (config == null)
                throw new SimpleBeanException(obj.GetType().Name + " dont has a SimpleConfig");
            if (config.getNumberOfFields() == 0)
                throw new SimpleBeanException("SimpleConfig dont have Fields");


            DbCommand cmd = connectcion.CreateCommand();
            cmd.CommandText = SqlHelper.CreateInsert(config);
            bool hasAutoIncrementPk = config.HasAutoIncrementFields();
            IList<DBField> fieldList = config.Fields;
            foreach (DBField field in fieldList) {
                if (!(field.Type is AutoIncrementType)) {
                    DbParameter param = cmd.CreateParameter();
                    param.IsNullable = false;
                    param.ParameterName = GetStrParam(field.getDbName());
                    object value = ReflectionUtils.GetPropertyValue(obj, field.getName());
                    cmd.Parameters.Add(param);
                    field.Type.BindToCommand(cmd,param.ParameterName,value);
                }
            }
            DbParameter paramId = cmd.CreateParameter();

            if (hasAutoIncrementPk) {

                paramId.ParameterName = "@GeneratedID";
                paramId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramId);
            }

            try {
                cmd.ExecuteNonQuery();
                if (hasAutoIncrementPk) {
                    ReflectionUtils.SetPropertyValue(obj, config.getFirstPK().getName(), paramId.Value);

                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool Add(object obj1, object obj2) {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool Remove(object obj1, object obj2) {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool Delete(object obj) {
            SimpleConfig config = ConfigManager.GetInstance().
                GetConfig(obj.GetType());
            if (config == null)
                throw new SimpleBeanException(obj.GetType().Name + " dont has a SimpleConfig");
            if (config.getNumberOfFields() == 0)
                throw new SimpleBeanException("SimpleConfig dont have Fields");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM ");
            IList<DBField> fieldList = config.Fields;

            int count = 0;

            sb.AppendLine(config.TableName);

            sb.AppendLine(" WHERE     ");
            if (!config.hasPK())
                throw new SimpleBeanException(obj.GetType().Name + " dont has a Pk Configured");

            IList<DBField> pkList = config.PKS;

            count = 0;


            foreach (DBField pk in pkList) {
                if (count > 0)
                    sb.Append(" AND ");
                sb.Append(pk.getDbName()).Append(" = ").Append(GetStrParam(pk.getDbName()));
                count++;
            }

            DbCommand cmd = connectcion.CreateCommand();
            cmd.CommandText = sb.ToString();

            foreach (DBField pk in pkList) {
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = GetStrParam(pk.getDbName());
                param.Value = ReflectionUtils.GetPropertyValue(obj, pk.getName());
                cmd.Parameters.Add(param);
            }

            try {
                return cmd.ExecuteNonQuery() > 0;
            } catch (Exception ex) {
                throw ex;
            }

        }





        public IList<T> LoadJoin<T>(object obj) {
            throw new Exception("The method or operation is not implemented.");
        }

        public int CountJoin(object obj, Type type) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList<T> LoadList<T>(T obj) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList<T> LoadList<T>(T obj, string orderBy) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList<T> LoadList<T>(T obj, int limit) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList<T> LoadList<T>(T obj, string orderBy, int limit) {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
        public DbTransaction BeginTransaction() {
            if (connectcion == null) {
                throw new SimpleBeanException();
            }
            return connectcion.BeginTransaction();

        }


    }


}
