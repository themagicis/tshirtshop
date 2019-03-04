using System;
using System.Collections.Generic;
using System.Linq;
using App.Common.DataAccess.Repository;

namespace App.Common.DataAccess
{
    public interface IDataContext : IDisposable, IUnitOfWork
    {
        IRepository<TEntity> Get<TEntity>() where TEntity : class;

        void ExecuteSql(string sql, object[] parameters);

        IQueryable<TList> FromLocalList<TList>(IList<TList> data);
    }
}
