using System;
using System.Text;

namespace SimpleCrud.Core {
    public class DBField {
        private readonly String name;

        private readonly IDBType type;

        private readonly String dbName;

        private readonly Boolean isPK;

        public bool Equals(DBField obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return Equals(obj.name, name) && Equals(obj.type, type) && Equals(obj.dbName, dbName) && obj.isPK.Equals(isPK) && obj.defaultToNow.Equals(defaultToNow);
        }

        public override int GetHashCode() {
            unchecked {
                int result = (name != null ? name.GetHashCode() : 0);
                result = (result*397) ^ (type != null ? type.GetHashCode() : 0);
                result = (result*397) ^ (dbName != null ? dbName.GetHashCode() : 0);
                result = (result*397) ^ isPK.GetHashCode();
                result = (result*397) ^ defaultToNow.GetHashCode();
                return result;
            }
        }

        private Boolean defaultToNow ;

        public DBField(String name, IDBType type)
            : this(name, name, type, false, null) {

        }

        public DBField(String name, IDBType type, Boolean isPK)
            : this(name, name, type, isPK, isPK ? name : null) {


        }

        public DBField(String name, IDBType type, Boolean isPK, String foreignName)
            : this(name, name, type, isPK, isPK ? foreignName : null) {


        }

        public DBField(String name, String dbName, IDBType type)
            : this(name, dbName, type, false, null) {


        }

        public DBField(String name, String dbName, IDBType type, Boolean isPK)
            : this(name, dbName, type, isPK, isPK ? dbName : null) {


        }

        public DBField(String name, String dbName, IDBType type, Boolean isPK, String foreignName) {

            this.name = name;

            this.dbName = dbName;

            this.type = type;

            this.isPK = isPK;
            
        }

        public bool IsPK {
            get {
                return isPK;
            }
        }

        public bool DefaultToNow {
            get { return defaultToNow; }
            set { defaultToNow = value; }
        }


        public String toString() {

            StringBuilder sb = new StringBuilder(32);

            sb.Append("DBField: ").Append(name).Append(" type=").Append(type).Append(" dbName=").Append(dbName);

            return sb.ToString();
        }


        public override Boolean Equals(Object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != typeof (DBField)) {
                return false;
            }
            return Equals((DBField) obj);
        }

        public String getName() {

            return name;

        }


        public IDBType Type {
            get { return type; }
        }

        public String getDbName() {

            return dbName;
        }


    }


}
