using CoastlineServer.DAL.Context;
using CoastlineServer.Repository.Exceptions;

namespace CoastlineServer.Repository
{
    public abstract class RepositoryBase
    {
        protected static OptimisticConcurrencyException<T> CreateOptimisticConcurrencyException<T>(
            CoastlineContext context, T entity)
            where T : class
        {
            var dbValues = context.Entry(entity)
                .GetDatabaseValues();
            return dbValues == null
                ? new OptimisticConcurrencyException<T>($"Update {typeof(T).Name}: entity not found")
                : new OptimisticConcurrencyException<T>($"Update {typeof(T).Name}: concurrency error",
                    (T) dbValues.ToObject());
        }
    }
}