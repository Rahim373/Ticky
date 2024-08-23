namespace Ticky.Domain.Entities;

public class Organization : BaseEntity
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public bool IsActive { get; set; }

    
    #region Virtual properties

    public virtual ApplicationUser Owner { get; set; }
    public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; }
    public virtual ICollection<Event> Events { get; set; }

    #endregion
}
