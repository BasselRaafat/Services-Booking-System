using BookingService.DAL.Data;
using BookingService.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services_Booking_System.View_Models;
using System.Globalization;

namespace Services_Booking_System.Controllers
{
    [Route("service")]
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
        [Route("ServiceDetails/{ServiceId:int}")]
        public IActionResult ServiceDetails([FromRoute] int ServiceId, string sortBy = "recommended", decimal? minPrice = null, decimal? maxPrice = null)
        {
            //ServiceId = 1;
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
                    TechnicianName = tech.FirstName + " " + tech.LastName,
                    TechnicianPrice = (decimal)tech.Price,
                    TechnicianReview = averageRating,
                });
            }



            // Apply price range filtering
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            if (minPrice.HasValue)
            {
                serviceProviders = serviceProviders.Where(sp => sp.TechnicianPrice >= minPrice.Value).ToList();
            }

            if (maxPrice.HasValue)
            {
                serviceProviders = serviceProviders.Where(sp => sp.TechnicianPrice <= maxPrice.Value).ToList();
            }



            //sorting //////

            SortBy(sortBy);
            switch (sortBy)
            {
                case "1": // Price DESC
                    serviceProviders = serviceProviders.OrderByDescending(sp => sp.TechnicianPrice).ToList();
                    break;
                case "2": // Price ASC
                    serviceProviders = serviceProviders.OrderBy(sp => sp.TechnicianPrice).ToList();
                    break;
                case "3": // Rating
                    serviceProviders = serviceProviders.OrderByDescending(sp => sp.TechnicianReview).ToList();
                    break;
                default:
                    break;
            }
            /////


            var serviceDetails = new ServiceDetailsViewModel()
            {
                ServiceTitle = serviceDb.Name,
                ServiceDescription = serviceDb.Description,
                ServiceProviders = serviceProviders
            };

            return View(serviceDetails);
        }



        public class SortItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public void SortBy(string sortBy)
        {
            List<SortItem> items = new List<SortItem> {
                new SortItem() {Id = 0, Name = "Recommended" },
                new SortItem() {Id = 1, Name = "Price DESC" },
                new SortItem() {Id = 2, Name = "Price ASC" },
                new SortItem() {Id = 3, Name = "Rating" }
            };
            ViewBag.SortItems = new SelectList(items, "Id", "Name", sortBy);
        }
    }
}
