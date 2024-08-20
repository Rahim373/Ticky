using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class DiscountMapping : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Code)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}
