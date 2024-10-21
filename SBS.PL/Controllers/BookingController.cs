using BookingService.BLL.Interfaces;
using BookingService.BLL.Repositories;
using BookingService.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services_Booking_System.View_Models;
using System.Security.Claims;
using WEBPage.Models.Identity;

namespace Services_Booking_System.Controllers;

public class BookingController : Controller
{
 private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBookingRepository _bookingRepo;
    private readonly IUserRepository _userRepo;
    private readonly ITechnicianRepository _techRepo;


    public BookingController(UserManager<ApplicationUser> userManager, IBookingRepository bookingRepo,IUserRepository userRepository, ITechnicianRepository technicianRepository)
    {
        _userManager = userManager;
        _bookingRepo = bookingRepo;
        this._userRepo = userRepository;
        _techRepo = technicianRepository;
    }

    private async Task<User> GetUserId()
    {
        var applicationUser = await _userManager.GetUserAsync(User);
        if (applicationUser == null)
            return null;

        //var email = applicationUser.Email;
        return _userRepo.GetByEmail(applicationUser.Email);
    }
    private async Task<Technician> GetTechnicianId()
    {
        var applicationUser = await _userManager.GetUserAsync(User);
        if (applicationUser == null)
            return null;

        //var email = applicationUser.Email;
        return _techRepo.GetByEmail(applicationUser.Email);
    }
    public async Task<IActionResult> UserBooking()
    {
        var user = await GetUserId();

        var books = _bookingRepo.GetWithUserId(user.Id);


        return View(books);
    }
    public async Task<IActionResult> Book(int technicianId)
    {

        var user = await GetUserId();
        if (user is not null)
        {
            var model = new BookingViewModel
            {
                TechnicianID = technicianId, // Technician ID from previous page
                UserID = user.Id, // Logged-in user's ID
                Status = "Pending"
            };
            return View(model);
        }
        return View();
    }

    [HttpPost]
    public IActionResult Book( BookingViewModel Book)

    {
        if (ModelState.IsValid)
        {
            var MappedBook = new Booking()
            {
                Date=Book.Date,
                Status = Book.Status,
                TechnicianID=Book.TechnicianID,
                UserID=Book.UserID,


            };
            
            _bookingRepo.Add(MappedBook);
            _bookingRepo.Save();
            return RedirectToAction("UserBooking");

        }

        return RedirectToAction("Index","Services");
    }

 
    //public IActionResult SelectTechnician(int technicianId)
    //{
    //    return RedirectToAction("TechnicianBookings", new { technicianId = technicianId });
    //}

    //[Authorize]
    public async Task<IActionResult> TechnicianBookings(string statusFilter)
    {
        var tech = await GetTechnicianId();
        var bookings = _bookingRepo.GetWithTechId(tech.Id);

        // Apply status filter if provided
        if (!string.IsNullOrEmpty(statusFilter))
        {
            bookings = bookings.Where(b => b.Status == statusFilter);
        }

        var bookingList = bookings.ToList();
        ViewBag.StatusFilter = statusFilter;  // Pass the filter value to the view

        return View(bookingList);
    }

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> UpdateBookingStatus(int bookingId, string newStatus,decimal? price)
    {
        if(newStatus == "Missed")
        {
            var booking = await _bookingRepo.GetById(bookingId);
            if (booking != null)
            {
                booking.Status = newStatus;
                booking.TotalPrice = 0;
                _bookingRepo.Save();
            }
        }
        if (price is null)
        {
            var booking = await _bookingRepo.GetById(bookingId);
            if (booking != null)
            {
                booking.Status = newStatus;
                _bookingRepo.Save();
            }
        }
        else
        {
            var booking = await _bookingRepo.GetById(bookingId);
            if (booking != null)
            {
                booking.Status = newStatus;
                booking.TotalPrice = price;
                _bookingRepo.Save();
            }
        }
        return RedirectToAction("TechnicianBookings");
    }

}
