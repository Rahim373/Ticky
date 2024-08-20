namespace Ticky.Domain.Entities;

public class EventAttendee : BaseEntity
{
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedOn { get; set; }


    #region Virtual properties

    public virtual ApplicationUser User { get; set; }
    public virtual Event Event { get; set; } 
    
    #endregion
}
