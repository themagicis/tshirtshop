using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App.Common.DataAccess.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbSet<T> databaseSet;

        private DbContext context;

        public GenericRepository(DbContext context)
        {
            if (context != null)
            {
                this.context = context;
                databaseSet = context.Set<T>();
            }
        }

        public virtual IQueryable<T> All => databaseSet;

        public virtual void Add(T entity)
        {
            databaseSet.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            databaseSet.Remove(entity);
        }

        public void AddRange(IEnumerable<T> items)
        {
            databaseSet.AddRange(items);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public T Find(params object[] keys)
        {
            return databaseSet.Find(keys);
        }

        public async Task BulkInsert(IList<T> items)
        {
            await context.BulkInsertAsync(items);
        }

        public async Task BulkInsert(IList<T> items, int batchSize, int timeoutSeconds)
        {
            var conf = new BulkConfig
            {
                BatchSize = batchSize,
                BulkCopyTimeout = timeoutSeconds
            };
            await context.BulkInsertAsync(items, conf);
        }

        public async Task BulkDelete(IList<T> items)
        {
            await context.BulkDeleteAsync(items);
        }

        public IQueryable<T> RunSql(string sql)
        {
            return databaseSet.FromSql(sql);
        }

        public IEnumerable<TResult> CollectionFromSql<TResult>(string sql, Dictionary<string, object> parameters) where TResult : new()
        {
            using (var cmd = context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;

                return ExecuteCommand<TResult>(cmd, parameters);
            }
        }

        public IEnumerable<TResult> CollectionFromStoredProc<TResult>(string name, Dictionary<string, object> parameters) where TResult : new()
        {
            using (var cmd = context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = name;

                return ExecuteCommand<TResult>(cmd, parameters);
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void SetContext(DbContext context)
        {
            if (context != null)
            {
                this.context = context;
                databaseSet = context.Set<T>();
            }
        }

        public void EnsureCreated()
        {
            if (context != null)
            {
                context.Database.EnsureCreated();
            }
        }

        private IEnumerable<TResult> ExecuteCommand<TResult>(DbCommand cmd, Dictionary<string, object> parameters) where TResult : new()
        {
            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            foreach (KeyValuePair<string, object> param in parameters)
            {
                if (param.Value != null)
                {
                    DbParameter parameter = cmd.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value;
                    cmd.Parameters.Add(parameter);
                }
            }

            var retObject = new List<TResult>();
            using (var dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var row = new TResult();
                    for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                    {
                        TrySetProperty(row, dataReader.GetName(fieldCount), dataReader[fieldCount]);
                    }

                    retObject.Add(new TResult());
                }
            }

            return retObject;
        }

        private void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value, null);
            }
        }
    }
}
