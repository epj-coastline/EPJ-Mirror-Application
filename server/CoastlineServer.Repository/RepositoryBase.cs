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
            T dbEntity = (T) context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new OptimisticConcurrencyException<T>($"Update {typeof(T).Name}: Concurrency-Fehler", dbEntity);
        }
    }
}