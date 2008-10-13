using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SimpleCrud.Core;

namespace SimpleCrud.Utils {
    public  class ReflectionUtils {

       public static object GetPropertyValue(object obj, string propertyName) {
           PropertyInfo property = GetPropertyInfo(obj, propertyName);
           return property.GetValue(obj, null);
       }

        public static PropertyInfo GetPropertyInfo(object obj, string propertyName) {
            Type type = obj.GetType();
            return type.GetProperty(propertyName);
        }

        public static void SetPropertyValue(object obj, string propertyName, object value) {
            PropertyInfo property = GetPropertyInfo(obj, propertyName);
            property.SetValue(obj, value,null);
        }
        public static Type GetPropertyType(object obj,string propertyName) {
            PropertyInfo propInfo = GetPropertyInfo( obj, propertyName);
            return propInfo.PropertyType;
        }
        public static void CallMethod(object obj,string methodName,  object[] values  ) {
            MethodInfo mInfo =  obj.GetType().GetMethod(methodName);
            
             mInfo.Invoke(obj,values);
        }
        public static bool IsCollection(Type type) {
            Type[] types;
            types = type.GetInterfaces();
            for(int i=0;i< types.Length ; i++) {
                if (types[i].Name.Equals(typeof(ICollection<>).Name) || types[i].Name.Equals(typeof(ICollection).Name) )
                return true;

            }
            return false;
        }
        public static bool FindItemInList(PropertyInfo propInfoList,object obj,SimpleConfig config, object item ) {
            string hashId = GetHashId(config, item);
            object list = GetPropertyValue(obj, propInfoList.Name);
            int count = (int) GetPropertyValue(list,"Count");
            
            for (int i =0;i < count;i++) {
                object it = propInfoList.PropertyType.GetProperties()[0].GetValue(list, new object[]{i});
                if (GetHashId(config,it).Equals(hashId)) {
                    return true;
                }
            }
            return false;
        }
        public static string GetHashId(SimpleConfig config, object item) {
            string hashId = "";
            foreach (DBField pk in config.PKS){
                hashId += GetPropertyValue(item, pk.getName());
            }
            return hashId;
        }

    }
}
