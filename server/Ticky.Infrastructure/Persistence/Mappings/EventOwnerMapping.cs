using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Application.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class EventOwnerMapping : IEntityTypeConfiguration<EventOwner>
{
    public void Configure(EntityTypeBuilder<EventOwner> builder)
    {
        builder.ToTable("EventOwners");

        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Event)
            .WithMany(e => e.Owners)
            .HasForeignKey(x => x.EventId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.User)
           .WithMany(e => e.AccessibleEvents)
           .HasForeignKey(x => x.UserId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.EventId, x.UserId, x.OwnerShipType })
            .IsUnique();
    }
}
