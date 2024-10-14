using BookingService.DAL.Data;
using BookingService.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Services_Booking_System.View_Models;

namespace Services_Booking_System.Controllers
{
    public class ServiceController : Controller
    {
        AppDbContext context;

        public ServiceController(AppDbContext db)
        {
            this.context = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ServiceDetails()
        {
            int ServiceId = 1;
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var serviceDb = context.Service.FirstOrDefault(x => x.Id == ServiceId);
            var technicianDb = context.Technician.Where(t => t.TechnicianService.Any(ts => ts.ServiceId == ServiceId)).ToList();
            if (serviceDb == null || technicianDb == null)
            {
                return View(ModelState);
            }
            var serviceProviders = new List<ServiceProviderViewModel>();
            foreach (var tech in technicianDb)
            {
                var reviews = context.Review.Where(r => r.TechnicianId == tech.Id).ToList();
                int reviewsCount = reviews.Any() ? reviews.Count : 0;
                decimal averageRating = (decimal)((reviewsCount > 0) ? Math.Round(reviews.Average(r => r.Rating), 2) : 0);

                serviceProviders.Add(new ServiceProviderViewModel
                {
                    ReviewsNum = reviewsCount,
                    TechnicianAddress = tech.Address,
                    TechnicianDescription = tech.Bio,
                    TechnicianImageUrl = tech.ImageUrl,
                    TechnicianName = tech.Name,
                    TechnicianPrice = (decimal)tech.Price,
                    TechnicianReview = averageRating,
                });
            }
            var serviceDetails = new ServiceDetailsViewModel()
            {
                ServiceTitle = serviceDb.Name,
                ServiceDescription = serviceDb.Description,
                ServiceProviders = serviceProviders
            };
            return View(serviceDetails);
        }
    }
}
