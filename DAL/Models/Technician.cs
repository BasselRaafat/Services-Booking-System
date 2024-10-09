

namespace BookingService.DAL.Models
{
    public class Technician
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string JobTitle { get; set; } 

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalID { get; set; }

        public string ImageUrl { get; set; }

        public string Certification { get; set; }
        public int BookingNumber { get; set; }

        public string Bio { get; set; }

        public string City { get; set; }

        public decimal Rating { get; set; }

        public string Address { get; set; }

        public int ReviewId { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public int BookingId { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }
}
