using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class EmailJobMapping : IEntityTypeConfiguration<EmailJob>
{
    public void Configure(EntityTypeBuilder<EmailJob> builder)
    {
        builder.ToTable("EmailJobs");

        builder.HasKey(t => t.Id);

        builder.HasIndex(x => x.To);
    }
}
