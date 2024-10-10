namespace BookingService.DAL.Models;

public class Category: ModelsBase
{
    public string Name { get; set; }
    public ICollection<Service> Services { get; set; }
}
