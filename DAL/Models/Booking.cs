using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.DAL.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public string TotalPrice { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        public User User { get; set; }

        [ForeignKey("Technician")]
        public string TechnicianID { get; set; }

        public Technician Technician { get; set; }
    }
}
