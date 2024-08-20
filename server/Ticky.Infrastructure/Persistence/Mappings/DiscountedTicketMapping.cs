using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class DiscountedTicketMapping : IEntityTypeConfiguration<DiscountedTicket>
{
    public void Configure(EntityTypeBuilder<DiscountedTicket> builder)
    {
        builder.ToTable("DiscountedTickets");

        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Discount)
            .WithMany(e => e.DiscountedTickets)
            .HasForeignKey(x => x.DisocuntId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Ticket)
           .WithMany(e => e.DiscountedTickets)
           .HasForeignKey(x => x.TicketId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(t => new {t.TicketId, t.DisocuntId})
            .IsUnique();
    }
}
