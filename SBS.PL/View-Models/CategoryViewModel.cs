using BookingService.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

namespace Services_Booking_System.View_Models;

public class CategoryViewModel
{
    public int CategoryId { get; set; }
    [Required(ErrorMessage ="Category's Name Is Requerd")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Category's Image Is Requerd")]
    public IFormFile Photo { get; set; }

    public string? PhotoName { get; set; }
    public ICollection<ServiceViewModel>? Services { get; set; }
}
