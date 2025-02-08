using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Entities.Base;
using RestaurantAPI.Domain.Interface.Repository.Base;
using RestaurantAPI.Infra.Context;

namespace RestaurantAPI.Infra.Repository.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected EntityContext _context;
        protected DbSet<TEntity> _dbSet;

        public BaseRepository(EntityContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> Get() => await _dbSet.ToListAsync();

        public virtual async Task<TEntity> GetById(long id) => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var savedEntity = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var savedEntity = _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> DeleteById(long id)
        {
            TEntity entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return entity;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
