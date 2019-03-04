using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Common.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All { get; }

        void Add(T entity);

        void AddRange(IEnumerable<T> items);

        void Remove(T entity);

        T Find(params object[] keys);

        Task BulkInsert(IList<T> items);

        Task BulkInsert(IList<T> items, int batchSize, int timeoutSeconds);

        Task BulkDelete(IList<T> items);

        void Update(T entity);

        void SaveChanges();

        IQueryable<T> RunSql(string sql);

        IEnumerable<TResult> CollectionFromSql<TResult>(string sql, Dictionary<string, object> parameters) where TResult : new();

        IEnumerable<TResult> CollectionFromStoredProc<TResult>(string name, Dictionary<string, object> parameters) where TResult : new();

        void SetContext(DbContext context);

        void EnsureCreated();
    }
}
