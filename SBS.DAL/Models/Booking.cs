using System.ComponentModel.DataAnnotations.Schema;
using WEBPage.Models.Identity;

namespace BookingService.DAL.Models;

public class Booking:ModelsBase
{

    public DateTime Date { get; set; }

    public string Status { get; set; }

    public decimal? TotalPrice { get; set; }

    [ForeignKey("User")]
    public int UserID { get; set; }

    //public string UserId { get; set; }

    [ForeignKey("Technician")]
    public int TechnicianID { get; set; }
    public User? User { get; set; }

    //public ApplicationUser? ApplicationUser { get; set; }
    public Technician? Technician { get; set; }
}
