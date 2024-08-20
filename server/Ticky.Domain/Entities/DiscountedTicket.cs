namespace Ticky.Domain.Entities;

public class DiscountedTicket : BaseEntity
{
    public Guid DisocuntId { get; set; }
    public Guid TicketId { get; set; }
    public DateTime CreatedOn { get; set; }


    #region Virtual properties

    public virtual Ticket Ticket { get; set; }
    public virtual Discount Discount { get; set; } 
    
    #endregion
}
