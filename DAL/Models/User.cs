

namespace BookingService.DAL.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    public string City { get; set; }

    public string Address { get; set; }

    public string NationalID { get; set; }

    public int BookingId { get; set; }
    public int ReviewId { get; set; }
    public ICollection<Booking> Bookings { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
