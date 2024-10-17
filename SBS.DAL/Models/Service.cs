namespace BookingService.DAL.Models;

public class Service : ModelsBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string BasePrice { get; set; }
    public string? PhotoName { get; set; }
    public int ServiceCategoryId { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<TechnicianService> TechnicianService { get; set; }
}
