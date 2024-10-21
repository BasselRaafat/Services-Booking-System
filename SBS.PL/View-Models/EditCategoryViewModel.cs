using Services_Booking_System.View_Models;
using System.ComponentModel.DataAnnotations;

namespace Services_Booking_System.View_Models;

public class EditCategoryViewModel
{
  public int CategoryId { get; set; }
    [Required(ErrorMessage ="Category's Name Is Requerd")]
    public string Name { get; set; }
   public IFormFile? Photo { get; set; }

    public string? PhotoName { get; set; }
    public ICollection<ServiceViewModel>? Services { get; set; }

}
