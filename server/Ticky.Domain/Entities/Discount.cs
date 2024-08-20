namespace Ticky.Domain.Entities;

public class Discount : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsEnabled { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime StartsOn { get; set; }
    public DateTime EndsOn { get; set; }

    #region Virtual properties
    
    public virtual ICollection<DiscountedTicket> DiscountedTickets { get; set; } 
    
    #endregion
}
