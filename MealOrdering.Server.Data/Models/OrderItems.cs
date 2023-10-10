namespace MealOrdering.Server.Data.Models;

public class OrderItems
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid CreateUserId { get; set; }
    public Guid OrderId { get; set; }
    public String Description { get; set; } 

    public virtual Users CreatedUser { get; set; }
    public virtual Orders Order { get; set; }
} 