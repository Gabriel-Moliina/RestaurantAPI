using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Entities.Base;
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();
            var now = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Local).ToLocalTime();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added && entry.Entity is BaseEntity added)
                {
                    added.CreatedAt = now;

                }
                else if (entry.State == EntityState.Modified && entry.Entity is BaseEntity modded)
                {
                    modded.ModifiedAt = now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
