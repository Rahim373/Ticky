using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class RegistrationInvitationMapping : IEntityTypeConfiguration<RegistrationInvitation>
{
    public void Configure(EntityTypeBuilder<RegistrationInvitation> builder)
    {
        builder.ToTable("RegistrationInvitations");

        builder.HasKey(t => t.Id);
    }
}
