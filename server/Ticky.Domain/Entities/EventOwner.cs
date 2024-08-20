using Ticky.Domain.Entities;
using Ticky.Domain.Enums;

namespace Ticky.Application.Entities;

public class EventOwner : BaseEntity
{
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public EventOwnerShipType OwnerShipType { get; set; }
    
    #region Virtual properties

    public virtual Event Event { get; set; }
    public virtual ApplicationUser User { get; set; } 
    
    #endregion
}
