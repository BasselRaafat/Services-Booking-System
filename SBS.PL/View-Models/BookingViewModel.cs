using BookingService.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Services_Booking_System.View_Models;

public class BookingViewModel
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Date is required")]

   // [DisplayFormat(DataFormatString = "{yyyy:mm:dd:HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }
    public string Status { get; set; }
    public decimal? TotalPrice { get; set; }
    public int  UserID { get; set; } // Retrieved from logged-in user
    //public int userId { get; set; }
    public int TechnicianID { get; set; } // Retrieved from previous page
    public User? User { get; set; }

    public Technician? Technician { get; set; }

}
