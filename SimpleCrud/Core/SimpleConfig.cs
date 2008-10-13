using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SimpleCrud.Types;

namespace SimpleCrud.Core {
    public class SimpleConfig {
        private readonly IList<DBField> fieldList = new List<DBField>();

        private readonly IList<DBField> pkList = new List<DBField>();

        private readonly IDictionary<Join, JoinConfig> joins = new Dictionary<Join, JoinConfig>();

        private readonly Type type;

        private readonly String tableName;

        private DBField sequence;

        private DBField autoincrement;

        public SimpleConfig(Type type, String tableName) {
            this.type = type;

            this.tableName = tableName;
        }

        public String TableName {
            get { return tableName; }
        }

        public IList<DBField> Fields {
            get {
                return fieldList;
            }
        }

        public IList<DBField> GetSequenceFields() {

            IList<DBField> list = new List<DBField>();
            foreach(DBField field in fieldList) {
                if (field.Type is SequenceType) {
                    list.Add(field);
                }
            }
            return list;
        }

        public IList<DBField> GetAutoIncrementFields() {

            IList<DBField> list = new List<DBField>();
            foreach(DBField field in fieldList) {
                if (field.Type is AutoIncrementType) {
                    list.Add(field);
                }
            }
            return list;
        }

        public bool HasAutoIncrementFields() {
            return GetAutoIncrementFields().Count > 0;
        }
        public bool HasAutoIncrementPK() {
            foreach(DBField field in GetAutoIncrementFields()) {
                if (field.IsPK)
                    return true;
            }
            return false;
        }




        public Type Type {
            get { return type; }
        }

        public JoinConfig getJoinConfig(Type myType) {
            Join j = new Join(this.type, myType);

            return joins[j];
        }

        public IList<JoinConfig> GetJoinConfigList() {
            IList<JoinConfig> list = new List<JoinConfig>();
            return new List<JoinConfig>(joins.Values);
        }

        public SimpleConfig AddJoin(JoinConfig join) {
            Join j = new Join(this.type, join.Type);
            //		KeyValuePair<Type,JoinConfig> kp = new KeyValuePair<Type, JoinConfig>();
            joins.Add(new KeyValuePair<Join, JoinConfig>(j, join));

            return this;
        }

        public SimpleConfig join(JoinConfig join) {
            return AddJoin(join);
        }

        public SimpleConfig AddJoin(Type type, String tableName, string propertyName, string foreginColumn, string pkColumn) {
            return AddJoin(new JoinConfig(type, tableName,propertyName,foreginColumn,pkColumn));
        }

        

        public SimpleConfig AddField(String name, IDBType dbType) {
            return AddField(name, name, dbType, false, null);
        }

        public SimpleConfig AddField(String name, String dbName, IDBType dbType) {
            return AddField(name, dbName, dbType, false, null);
        }

        public SimpleConfig AddField(String name, IDBType dbType, bool isPK) {
            return AddField(name, name, dbType, isPK, isPK ? name : null);
        }

        public SimpleConfig AddField(String name, IDBType dbType, bool isPK, String foreignName) {
            return AddField(name, name, dbType, isPK, isPK ? foreignName : null);
        }

        public SimpleConfig AddField(String name, String dbName, IDBType dbType, bool isPK) {
            return AddField(name, dbName, dbType, isPK, isPK ? dbName : null);
        }

        private DBField findDBField(String name) {
            foreach (DBField f in fieldList) {
                if (f.getName().Equals(name)) {
                    return f;
                }
            }


            return null;
        }

        public SimpleConfig AddField(String name, String dbName, IDBType dbType, Boolean isPK, String foreignName) {
            DBField f = new DBField(name, dbName, dbType, isPK);

            fieldList.Add(f);

            if (isPK) {
                pkList.Add(f);

                if (type is SequenceType && sequence == null) {
                    sequence = f;
                }
                else if (type is AutoIncrementType && autoincrement == null) {
                    autoincrement = f;
                }
            }

            return this;
        }

        public DBField getAutoIncrementField() {
            return autoincrement;
        }

        public DBField getSequenceField() {
            return sequence;
        }

        public SimpleConfig field(String name, String dbName, IDBType dbType) {
            return AddField(name, dbName, dbType);
        }

        public SimpleConfig field(String name, String dbName, IDBType type, Boolean isPK) {
            return AddField(name, dbName, type, isPK);
        }

        public SimpleConfig pk(String name, IDBType type) {
            return AddField(name, type, true);
        }

        public SimpleConfig defaultToNow(String name) {
            DBField f = findDBField(name);

            if (f == null) {
                throw new ArgumentException("Cannot find field with name: " + name);
            }

            f.DefaultToNow = true;

            return this;
        }

        public SimpleConfig field(String name, String dbName, IDBType dbType, Boolean isPK, String foreignName) {
            return AddField(name, dbName, dbType, isPK, foreignName);
        }


        public SimpleConfig pk(String name, String dbName, IDBType dbType) {
            return AddField(name, dbName, dbType, true);
        }

        public SimpleConfig field(String name, IDBType dbType) {
            return AddField(name, dbType);
        }

        public SimpleConfig field(String name, IDBType dbType, Boolean isPK) {
            return AddField(name, dbType, isPK);
        }

        public SimpleConfig field(String name, IDBType dbType, Boolean isPK, String foreignName) {
            return AddField(name, dbType, isPK, foreignName);
        }

        public SimpleConfig pk(String name, IDBType dbType, String foreignName) {
            return AddField(name, dbType, true, foreignName);
        }

        public SimpleConfig pk(String name, String dbName, IDBType dbType, String foreignName) {
            return AddField(name, dbName, dbType, true, foreignName);
        }

        public int getNumberOfFields() {
            return fieldList.Count;
        }

        public int getNumberOfPKs() {
            return pkList.Count;
        }

        public String toString() {
            StringBuilder sb = new StringBuilder(64);


            return sb.ToString();
        }


        public Boolean hasPK() {
            return (pkList != null && pkList.Count != 0);
        }

        public DBField getFirstPK() {
            return pkList[0];
        }

        public IList<DBField> PKS {
            get { return pkList; }
        }

        private class Join {
            private readonly Type type1;

            private readonly Type type2;

            public Join(Type t1, Type t2) {
                type1 = t1;

                type2 = t2;
            }

            public override int GetHashCode() {
                return type1.GetHashCode()*31 + type2.GetHashCode();
            }

            public override bool Equals(Object obj) {
                if (obj is Join) {
                    Join j = (Join) obj;

                    if (j.type1.Equals(this.type1) && j.type2.Equals(this.type2)) {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}