using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Mappings;

public class ApplicationUserMapping : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var guid = Guid.Parse("02d767a9-23d5-45f0-a96a-ccbc64bb8a19");
        var user = new ApplicationUser
        {
            Id = guid,
            Email = "rahim.prsf@gmail.com",
            UserName = "rahim.prsf@gmail.com",
            NormalizedEmail = "rahim.prsf@gmail.com",
            NormalizedUserName = "rahim.prsf@gmail.com",
            SecurityStamp = guid.ToString()
        };
        user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, "Temppass1!");
        builder.HasData(user);
    }
}
