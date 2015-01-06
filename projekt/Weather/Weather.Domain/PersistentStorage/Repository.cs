using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Weather.Domain.PersistentStorage
{
    /// <summary>
    /// Generic repository for communicating with a database
    /// Using Entity Framework
    /// </summary>
    /// <typeparam name="T">A instance of a class</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _context;

        private DbSet<T> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        /// <summary>
        /// Creates a SQL SELECT query against the DbSet
        /// </summary>
        /// <param name="filter">WHERE statement</param>
        /// <param name="orderBy">ORDER BY statement</param>
        /// <param name="includeProperties">Properties of the object</param>
        /// <returns>A collection of objects</returns>
        public IEnumerable<T> Select(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<T> query = _set;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy == null ? query.ToList() : orderBy(query).ToList();
        }

        /// <summary>
        /// Selects object with the given Id from the DbSet
        /// </summary>
        /// <param name="id">Id to select</param>
        /// <returns>An object</returns>
        public T SelectById(object id)
        {
            return _set.Find(id);
        }

        /// <summary>
        /// Inserts the given object to the DbSet
        /// </summary>
        /// <param name="entityToAdd">The object to insert</param>
        public void Insert(T entityToAdd)
        {
            _set.Add(entityToAdd);
        }

        /// <summary>
        /// Updates properties in the given object in the DbSet
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public void Update(T entityToUpdate)
        {
            _set.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes an object from the DbSet
        /// </summary>
        /// <param name="id">Id of the object to delete</param>
        public void Delete(object id)
        {
            T entityToDelete = _set.Find(id);
            _set.Remove(entityToDelete);
        }
    }
}
