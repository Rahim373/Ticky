using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class OrganizationMapping : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.HasOne(t => t.Owner)
            .WithOne(e => e.Organization)
            .HasForeignKey<Organization>(x => x.OwnerId);
    }
}
