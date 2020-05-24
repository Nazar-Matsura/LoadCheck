using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LoadCheck.Infrastructure.Persistence
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetSingle(Guid id);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression);
        Task AddOrUpdate(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task Remove(TEntity entity);
        Task RemoveById(Guid id);
        Task RemoveRange(IEnumerable<TEntity> entities);
        Task<bool> Any(Expression<Func<TEntity, bool>> expression);
    }
}