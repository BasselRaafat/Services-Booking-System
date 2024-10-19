using BookingService.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Services_Booking_System.View_Models;

public class ServiceViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Name Is Required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Description Is Required")]
    public string Description { get; set; }
    public string? BasePrice { get; set; }
    public IFormFile Photo { get; set; }
    public string? PhotoName { get; set; }
    public int? ServiceCategoryId { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<TechnicianService>? TechnicianService { get; set; }
}
