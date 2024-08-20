using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class TicketMapping : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Event)
            .WithMany(e => e.Tickets)
            .IsRequired()
            .HasForeignKey(x => x.EventId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
