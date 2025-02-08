using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository.Base;

namespace RestaurantAPI.Domain.Interface.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> Exists(string email);
        Task<User> Login(string email, string password);
    }
}
