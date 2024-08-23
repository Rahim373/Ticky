using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class OrganizationMemberMapping : IEntityTypeConfiguration<OrganizationMember>
{
    public void Configure(EntityTypeBuilder<OrganizationMember> builder)
    {
        builder.ToTable("OrganizationMembers");

        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Organization)
            .WithMany(e => e.OrganizationMembers)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.User)
           .WithMany(e => e.AccessibleOrganizations)
           .HasForeignKey(x => x.UserId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(t => new { t.UserId, t.OrganizationId})
            .IsUnique();
    }
}
