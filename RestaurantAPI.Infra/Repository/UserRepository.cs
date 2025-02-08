using RestaurantAPI.CrossCutting.Cryptography;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Infra.Context;
using RestaurantAPI.Infra.Repository.Base;

namespace RestaurantAPI.Infra.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(EntityContext context) : base(context)
        {
        }

        public async Task<User> Create(User user)
        {
            user.Password = user.Password.Crypt();
            return await Add(user);
        }
    }
}
