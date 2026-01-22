using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Films.Domain.Entities;

namespace Films.Infrastructure.Data.Configurations;

public class FilmConfiguration : IEntityTypeConfiguration<Film>
{
    public void Configure(EntityTypeBuilder<Film> builder)
    {
        builder.ToTable("Films");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(f => f.Description)
            .HasMaxLength(1000);

        builder.Property(f => f.Country)
            .HasMaxLength(100);

        builder.Property(f => f.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(f => f.CreatedBy)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValue("System");

        builder.Property(f => f.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(f => f.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(f => f.Name);
    }
}
