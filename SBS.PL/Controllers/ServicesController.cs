using BookingService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Services_Booking_System.Controllers;

public class ServicesController : Controller
{
    private readonly IServiceRepository _serviceRepo;
    public ServicesController(IServiceRepository serviceRepo) 
    {
        _serviceRepo = serviceRepo;
    }
    public IActionResult Index(int id)
    {
        var services =_serviceRepo.GetByCategoryId(id);
        ViewBag.CategoryId = id;
        return View(services);
    }
}
