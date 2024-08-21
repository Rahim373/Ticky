using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence;

public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, Guid>,
    IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) { }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventAttendee> EventAttendees { get; set; }
    public DbSet<EventOwner> EventOwners { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<DiscountedTicket> DiscountedTickets { get; set; }

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
