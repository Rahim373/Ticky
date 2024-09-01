using Microsoft.AspNetCore.Identity;

namespace Ticky.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public bool PendingOrgCreation { get; set; }

    #region Virtual properties
    
    public virtual ICollection<Event> CreatedEvents { get; set; }
    public virtual ICollection<EventOwner> AccessibleEvents { get; set; }
    public virtual ICollection<EventAttendee> Participations { get; set; }
    public virtual Organization Organization { get; set; }
    public virtual ICollection<OrganizationMember> AccessibleOrganizations { get; set; }

    #endregion
}
