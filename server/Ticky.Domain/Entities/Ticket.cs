using Ticky.Domain.Enums;

namespace Ticky.Domain.Entities;

public class Ticket : BaseEntity
{
    public string Name { get; set; }
    public Guid EventId { get; set; }
    public int QtyAvailable { get; set; }
    public int Minimum { get; set; }
    public int Max { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; } = DateTime.Now;

    public TicketStatus TicketStatus { get; set; }

    
    #region Virtual properties

    public virtual Event Event { get; set; }
    public virtual ICollection<DiscountedTicket> DiscountedTickets { get; set; }

    #endregion
}

