using App.Common.DataAccess.Repository;
using EFCore.MemoryJoin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Common.DataAccess
{
    public class DataContext<T> : IDataContext where T : DbContext
    {
        protected T context;

        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IRepository<TEntity> Get<TEntity>() where TEntity : class
        {
            if (!repositories.ContainsKey(typeof(TEntity)))
            {
                repositories.Add(typeof(TEntity), new GenericRepository<TEntity>(context));
            }

            return (IRepository<TEntity>)repositories[typeof(TEntity)];
        }

        public void ExecuteSql(string sql, params object[] parameters)
        {
            context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IQueryable<TList> FromLocalList<TList>(IList<TList> data)
        {
            return context.FromLocalList(data);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }

        public void EnsureCreated()
        {
            context.Database.EnsureCreated();
        }

        public void EnsureDeleted()
        {
            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// For testing purposes
        /// </summary>
        /// <param name="ctx"></param>
        public void SetContext(T ctx)
        {
            context = ctx;
        }

        public void Migrate()
        {
            if (context != null)
            {
                context.Database.Migrate();
            }
        }
    }
}
