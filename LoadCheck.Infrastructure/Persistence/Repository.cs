using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LoadCheck.Core.Entities;

namespace LoadCheck.Infrastructure.Persistence
{
    // Copy from my other pet project
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity: class, IIdentifiable
    {
        protected readonly ApplicationDbContext _dbContext;

        protected readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetSingle(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return _entities;
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities
                .Where(expression)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllByIds(List<Guid> ids)
        {
            return await _entities
                .Where(e => ids.Contains(e.Id))
                .ToListAsync();
        }
        
        public async Task AddOrUpdate(TEntity entity)
        {
            //_entities.AddOrUpdate(entity);

            _entities.Add(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(TEntity entity)
        {
            _entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveById(Guid id)
        {
            var entity = await GetSingle(id);
            await Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.AnyAsync(expression);
        }
    }
}
