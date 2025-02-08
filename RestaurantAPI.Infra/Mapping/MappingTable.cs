using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Infra.Mapping
{
    internal class MappingTable : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("tables");

            builder.Property(p => p.Id)
                .HasColumnName(nameof(Table.Id).ToLower());

            builder.Property(p => p.Identification)
                .HasColumnType("varchar(100)")
                .HasColumnName(nameof(Table.Identification).ToLower());

            builder.Property(p => p.Capacity)
                .HasColumnType("int")
                .HasColumnName(nameof(Table.Capacity).ToLower());
            
            builder.Property(p => p.Status)
                .HasColumnType("int")
                .HasColumnName(nameof(Table.Status).ToLower());

            builder.Property(p => p.RestaurantId)
                .HasColumnType("bigint")
                .HasColumnName(nameof(Table.Restaurant).ToLower() + "_id");

            builder.HasOne(r => r.Restaurant)
                .WithMany(e => e.Tables)
                .HasForeignKey(e => e.RestaurantId);
        }
    }
}
