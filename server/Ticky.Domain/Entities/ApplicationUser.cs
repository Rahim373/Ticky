using Microsoft.AspNetCore.Identity;
using Ticky.Application.Entities;

namespace Ticky.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    #region Virtual properties
    
    public virtual ICollection<Event> CreatedEvents { get; set; }
    public virtual ICollection<EventOwner> AccessibleEvents { get; set; }
    public virtual ICollection<EventAttendee> Participations { get; set; } 

    #endregion
}
