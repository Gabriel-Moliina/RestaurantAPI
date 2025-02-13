using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Infra.Mapping
{
    public class MappingRestaurant : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("restaurants");

            builder.Property(p => p.Id)
                .HasColumnName(nameof(Restaurant.Id).ToLower());

            builder.Property(p => p.Name)
                .HasColumnType("varchar(100)")
                .HasColumnName(nameof(Restaurant.Name).ToLower());

            builder.Property(p => p.UserId)
                .HasColumnType("bigint")
                .HasColumnName(nameof(Restaurant.User).ToLower() + "_id");

            builder.HasOne(r => r.User)
                .WithMany(e => e.Restaurants)
                .HasForeignKey(e => e.UserId);
        }
    }
}