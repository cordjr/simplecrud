using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SimpleCrud.Core {
    interface ISimpleSession {

        

         bool Get(Object obj);


        void GetWithFulljoin(Object obj);

         ///<summary>
         /// Update the obj in the database. Only the fields that have
         /// been modified (dirty) will be updated.
         /// 
         /// It will return 1 if an Update did happen or 0 if the obj could not
         /// be found in the database or if there was nothing to Update in the obj.
         /// 
         /// The obj MUST have its primary key set, otherwise it is impossible to Update
         /// the obj in the database, and an exception will be thrown.
         /// </summary>
         /// <param name="obj">The obj to be updated</param>
        int Update(Object obj);

        int Update(Object obj, Boolean dynaUpdate);

         ///<summary>
         /// Insert the obj in the database.
         /// 
         /// Depending on the type of PK, the generation of the PK can and
         /// should be taken care by the DB itself. The generated PK should
         /// be inserted in the obj by reflection.
         /// 
         /// The default, database-independent implementation of this method
         /// must only Insert all fields in the database not worrying about
         /// PK generation issues.
         /// </summary>
        /// <param name="obj"obj The obj to Insert></param>
        void Insert(Object obj);

        ///<summary>
         /// Add a dependency based on a relationship OneToMany/ManyToMany.
         /// 
         /// Returns true if the dependency was really added or false if it
         /// was already there.
         /// </summary>
         /// <param name="obj1"></param>
         /// <param name="obj2"></param>
        Boolean Add(Object obj1, Object obj2);

         ///<summary>
         /// Remove a dependency based on a relationship 
         /// OneToMany/ManyToMany.
         /// 
         /// Returns true if the dependency was really removed or false if it
         /// was not there.
         /// </summary>
         /// <param name="obj1"></param>
         /// <param name="obj2"></param>
         /// <returns>true if the dependency was really removed or false if it was not there.</returns> 
        Boolean Remove(Object obj1, Object obj2);

        ///<summary>
        /// Delete the obj from the database. 
        /// The PK of the obj MUST be set.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if it was deleted or false if it did not exist</returns> 
        Boolean Delete(Object obj);

        ///<summary>
        /// Create a transaction for this session.
        /// </summary>
        /// <returns>DBTransaction</returns>
        DbTransaction BeginTransaction();
        

        IList<T> LoadJoin<T>(Object obj);

        int CountJoin(Object obj, Type type);

        IList<T> LoadList<T>(T obj);

        IList<T> LoadList<T>(T obj, String orderBy);

        IList<T> LoadList<T>(T obj, int limit);

        IList<T> LoadList<T>(T obj, String orderBy, int limit);

        }
    }
