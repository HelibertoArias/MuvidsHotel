using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muvids.Domain.Entities;
using System;

namespace Muvids.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(x => x.Id);

        builder.Property(x => x.Start)
            .IsRequired();

        builder.Property(x => x.IsDeleted);

        builder.Property(x => x.IsActive);

        // Auditable Entity
        builder.Property(x => x.CreatedBy)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.LastModifiedBy)
           .HasColumnType("varchar(200)");

        builder.Property(x => x.LastModifiedDate);

        // Navigation
        builder.HasOne(x => x.Room)
                .WithMany(x => x.Bookings)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.RoomId);

        // Use the Task List: https://bit.ly/3GXzyPX
        // TODO : Set only in DEBUG mode
        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Booking> builder)
    {
        builder.HasData(
            new Booking
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Start = new DateTime(2022, 1, 1),
                End = new DateTime(2022, 1, 1).AddDays(1),
                RoomId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991850"),
                IsActive = true,
                IsDeleted = false,
                CreatedBy = "Dummy",
                CreatedDate = DateTime.Now
            },
            new Booking
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"),
                Start = new DateTime(2022, 2, 1),
                End = new DateTime(2022, 2, 1).AddDays(2),
                RoomId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991850"),
                IsActive = true,
                IsDeleted = false,
                CreatedBy = "Dummy",
                CreatedDate = DateTime.Now
            });
    }
}
