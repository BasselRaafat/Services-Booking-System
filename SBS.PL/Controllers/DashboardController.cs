using BookingService.BLL.Repositories;
using BookingService.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services_Booking_System.View_Models;
using WEBPage.Models.Identity;

namespace Services_Booking_System.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly TechnicianRepository technicianRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly ServiceRepository serviceRepository;
        private readonly BookingRepository bookingRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;


        public DashboardController(UserRepository userRepository,TechnicianRepository technicianRepository
            ,CategoryRepository categoryRepository,ServiceRepository serviceRepository, BookingRepository bookingRepository,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userRepository = userRepository;
            this.technicianRepository = technicianRepository;
            this.categoryRepository = categoryRepository;
            this.serviceRepository = serviceRepository;
            this.bookingRepository = bookingRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.UserCount = await userRepository.CountAllAsync();
            dashboardViewModel.TechnicianCount = await technicianRepository.CountAllAsync();
            var admin = await userManager.GetUsersInRoleAsync("Admin");
            dashboardViewModel.AdminsCount = admin.Count();
            dashboardViewModel.CategoriesCount = await categoryRepository.CountAllAsync();
            dashboardViewModel.ServicesCount = await serviceRepository.CountAllAsync();
            dashboardViewModel.NumberOfBookings = await bookingRepository.CountAllAsync();

            return View("Index",dashboardViewModel);            
        }
        public async Task<IActionResult> Admin()
        {
            IList<ApplicationUser> AdminList = await userManager.GetUsersInRoleAsync("Admin");

            return View("Admin",AdminList);
        }
        public  IActionResult EditUser()
        {
            return View();
        }
        public IActionResult DeleteUser()
        {
            return View();
        }
    }
}
