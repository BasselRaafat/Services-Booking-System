using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.DAL.Models;

public class TechnicianService
{
    [ForeignKey("Service")]

    public int ServiceId { get; set; }


    [ForeignKey("Technician")]
    public int TechnicianId { get; set; }

    public double Price { get; set; }


    public Technician Technician { get; set; }
    public Service Service { get; set; }
}
