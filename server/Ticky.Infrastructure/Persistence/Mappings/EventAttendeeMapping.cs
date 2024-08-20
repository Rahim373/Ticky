using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class EventAttendeeMapping : IEntityTypeConfiguration<EventAttendee>
{
    public void Configure(EntityTypeBuilder<EventAttendee> builder)
    {
        builder.ToTable("EventAttendees");

        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Event)
            .WithMany(e => e.Attendees)
            .HasForeignKey(x => x.EventId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.User)
           .WithMany(e => e.Participations)
           .HasForeignKey(x => x.UserId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.EventId, x.UserId })
            .IsUnique();
    }
}
