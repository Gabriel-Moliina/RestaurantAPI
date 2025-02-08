using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Entities.Base;
using RestaurantAPI.Domain.Interface.Repository.Base;
using RestaurantAPI.Infra.Context;

namespace RestaurantAPI.Infra.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected EntityContext _context;
        protected DbSet<TEntity> _dbSet;

        public RepositoryBase(EntityContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> Get() => await _dbSet.ToListAsync();

        public async Task<TEntity> GetById(long id) => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        public async Task<TEntity> Add(TEntity entity)
        {
            var savedEntity = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var savedEntity = _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return savedEntity.Entity;
        }

        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteById(long id)
        {
            TEntity entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
