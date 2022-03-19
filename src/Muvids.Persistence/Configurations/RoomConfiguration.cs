using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muvids.Domain.Entities;
using System;

namespace Muvids.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.Property(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

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
        builder.HasMany(x => x.Bookings)
                .WithOne(x => x.Room)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.RoomId);

        // Use the Task List: https://bit.ly/3GXzyPX
        // TODO : Set only in DEBUG mode
        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<Room> builder)
    {
        builder.HasData(
          new Room
          {
              Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991850"),
              Name = "Room 01",
              IsDeleted = false,
              CreatedBy = "Dummy",
              CreatedDate = DateTime.Now
          });
    }
}
