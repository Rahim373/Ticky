using Microsoft.AspNetCore.Identity;

namespace Ticky.Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public Guid? Extends { get; set; }
}
