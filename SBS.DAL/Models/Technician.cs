﻿

namespace BookingService.DAL.Models;

public class Technician : ModelsBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; } 
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string NationalID { get; set; }
    public string ImageUrl { get; set; }
    public int? BookingNumber { get; set; }
    public string Bio { get; set; }
    public string City { get; set; }
    public DateTime Startfrom { get; set; }
    public DateTime StartTo { get; set; }
    public string Address { get; set; }
    public double Price { get; set; }
    public decimal Rating { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Booking> Bookings { get; set; }
    public ICollection<TechnicianService> TechnicianService { get; set; }

}
