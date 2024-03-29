﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Weather.Domain.PersistentStorage
{
    /// <summary>
    /// Interface for generic repository
    /// </summary>
    /// <typeparam name="T">Tyoe of database object</typeparam>
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Select(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T SelectById(object id);
        
        void Insert(T entityToAdd);
        
        void Update(T entityToUpdate);
        
        void Delete(object id);
    }
}
