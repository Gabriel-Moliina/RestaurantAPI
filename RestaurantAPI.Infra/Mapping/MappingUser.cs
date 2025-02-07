using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Infra.Mapping
{
    internal class MappingUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(p => p.Id)
                .HasColumnName(nameof(User.Id).ToLower());

            builder.Property(p => p.Name)
                .HasColumnType("varchar(100)")
                .HasColumnName(nameof(User.Name).ToLower());

            builder.Property(p => p.Email)
                .HasColumnType("varchar(100)")
                .HasColumnName(nameof(User.Email).ToLower());
            
            builder.Property(p => p.Password)
                .HasColumnType("varchar(100)")
                .HasColumnName(nameof(User.Password).ToLower());
        }
    }
}
