using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Infra.Mapping;

namespace RestaurantAPI.Infra.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MappingReservation());
            modelBuilder.ApplyConfiguration(new MappingRestaurant());
            modelBuilder.ApplyConfiguration(new MappingTable());
            modelBuilder.ApplyConfiguration(new MappingUser());
        }
    }
}
