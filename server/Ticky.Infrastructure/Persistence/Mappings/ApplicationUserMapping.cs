using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class ApplicationUserMapping : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("AspNetUsers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.PendingOrgCreation)
            .HasDefaultValue(false);
    }
}
