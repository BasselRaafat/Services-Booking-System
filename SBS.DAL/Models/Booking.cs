using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.DAL.Models;

public class Booking:ModelsBase
{

    public DateTime Date { get; set; }

    public string Status { get; set; }

    public string TotalPrice { get; set; }

    [ForeignKey("User")]
    public int UserID { get; set; }


    [ForeignKey("Technician")]
    public int TechnicianID { get; set; }
    public User User { get; set; }

    public Technician Technician { get; set; }
}
