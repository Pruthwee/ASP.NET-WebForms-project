using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Films.Domain.Entities;

namespace Films.Infrastructure.Data.Configurations;

public class ActorConfiguration : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.ToTable("Actors");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Description)
            .HasMaxLength(500);

        builder.Property(a => a.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(a => a.CreatedBy)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValue("System");

        builder.Property(a => a.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(a => a.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(a => a.Sex)
            .WithMany(s => s.Actors)
            .HasForeignKey(a => a.SexId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(a => a.Name);
    }
}
