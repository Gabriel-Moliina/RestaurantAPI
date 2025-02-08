using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Infra.Mapping
{
    public class MappingReservation : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("reservations");

            builder.Property(p => p.Id)
                .HasColumnName(nameof(Reservation.Id).ToLower());

            builder.Property(p => p.Email)
                .HasColumnType("varchar(255)")
                .HasColumnName(nameof(Reservation.Email).ToLower());

            builder.Property(p => p.Date)
                .HasColumnType("datetime")
                .HasColumnName(nameof(Reservation.Date).ToLower());

            builder.Property(p => p.TableId)
                .HasColumnType("bigint")
                .HasColumnName(nameof(Reservation.Table).ToLower() + "_id");

            builder.HasOne(r => r.Table)
                .WithOne(e => e.Reservation)
                .HasForeignKey<Reservation>(e => e.TableId);
        }
    }
}
