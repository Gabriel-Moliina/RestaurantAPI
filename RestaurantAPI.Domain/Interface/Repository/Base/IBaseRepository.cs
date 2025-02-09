using RestaurantAPI.Domain.Entities.Base;

namespace RestaurantAPI.Domain.Interface.Repository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> Get();
        Task<TEntity> GetById(long id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> DeleteById(long id);
        Task<bool> ExistsById(long id);
    }
}
