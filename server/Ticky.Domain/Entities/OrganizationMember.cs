namespace Ticky.Domain.Entities;

public class OrganizationMember : BaseEntity
{
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public bool IsActive { get; set; }


    #region Virtual properties
    
    public virtual Organization Organization { get; set; }
    public virtual ApplicationUser User { get; set; } 
    
    #endregion
}
