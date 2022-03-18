using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muvids.Domain.Entities;

namespace Muvids.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(x => x.Id);
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(x => x.CreatedBy)
            .IsRequired();
        builder.Property(x => x.CreatedDate)
            .IsRequired();


    }
}
