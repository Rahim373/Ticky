using Ticky.Domain.Enums;

namespace Ticky.Domain.Entities;

public class Event : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
    public EventStatus EventStatus { get; set; }

    #region Virtual properties
    
    public virtual ICollection<Ticket> Tickets { get; set; }
    public virtual ICollection<EventOwner> Owners { get; set; }
    public virtual ICollection<EventAttendee> Attendees { get; set; }
    public virtual ApplicationUser CreatedByUser { get; set; }

    #endregion

    public Event() { }

    public Event(string name, string description, EventStatus status, Guid createdByUserId)
    {
        Name = name?.Trim();
        Description = description?.Trim();
        EventStatus = status;
        CreatedOn = DateTime.UtcNow;
        UpdatedOn = DateTime.UtcNow;
    }
}
