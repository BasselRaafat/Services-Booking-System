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
        public IActionResult AddAdmin()
        {
            return View("AddAdmin");
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(AddAdminViewModel addAdminViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Address = addAdminViewModel.Address;
                user.FirstName = addAdminViewModel.FirstName;
                user.LastName = addAdminViewModel.LastName;
                user.Email = addAdminViewModel.Email;
                user.UserName = addAdminViewModel.Email;
                var identityresult = await userManager.CreateAsync(user, addAdminViewModel.Password);
                if(!identityresult.Succeeded)
                {
                    foreach(var item in identityresult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View("AddAdmin",addAdminViewModel);
                }
                await userManager.AddToRoleAsync(user, "Admin");
                return RedirectToAction("Index");
            }
            return View("AddAdmin", addAdminViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"User with email {email} not found.");
            }

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok($"User with email {email} deleted successfully.");
            }

            return BadRequest("Error occurred while deleting the user.");
        }

    }
}
