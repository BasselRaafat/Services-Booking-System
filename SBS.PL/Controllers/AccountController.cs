using BookingService.BLL.Services;
using BookingService.DAL.Data;
using BookingService.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services_Booking_System.View_Models;
using System.Linq;
using System.Security.Claims;
using WEBPage.Models.Identity;
using WEBPage.View_Models;
using WEBPage.View_Models.WEBPage.View_Models;

namespace WEBPage.Controllers
{
    public class AccountController : Controller
    {
        private readonly TechnicianServices technicianService;

        private readonly UserManager<ApplicationUser> UserManager;

        private readonly SignInManager<ApplicationUser> SignInManager;

        private readonly UserService UserService;

        private AppDbContext context;

        public AccountController(AppDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UserService userService, TechnicianServices technicianService)
        {
            this.context = context;
            UserManager = userManager;
            SignInManager = signInManager;
            UserService = userService;
            this.technicianService = technicianService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register
            (RegisterViewModel registerView)
        {
            if (ModelState.IsValid && !registerView.IsTechnician)
            {
                ApplicationUser user = new ApplicationUser();
                user.Address = registerView.Address;
                user.FirstName = registerView.FirstName;
                user.LastName = registerView.LastName;
                user.Email = registerView.Email;
                user.PhoneNumber = registerView.PhoneNumber;
                user.UserName = registerView.Email;

                IdentityResult identityResult = await UserManager.CreateAsync(user, registerView.Password);
                if (identityResult.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "User");
                    var userid = await UserService.CreateUser(registerView.FirstName, registerView.LastName, registerView.Email,
                       registerView.PhoneNumber, registerView.City, registerView.Address);

                    if (userid == 0)
                    {
                        await UserManager.DeleteAsync(user);
                        ModelState.AddModelError("", "Error");
                        return View(registerView);
                    }


                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("FirstName", registerView.FirstName));
                    await SignInManager.SignInWithClaimsAsync(user, false, claims);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View("Register", registerView);
            }

            //-------------------

            if (registerView.IsTechnician)
            {
                if (string.IsNullOrEmpty(registerView.JobTitle))
                {
                    ModelState.AddModelError("JobTitle", "Job Title is required for technicians.");
                }
                if (string.IsNullOrEmpty(registerView.NationalID) || registerView.NationalID.Length != 14)
                {
                    ModelState.AddModelError("NationalID", "National ID is required and must be 14 characters long for technicians.");
                }
                if (registerView.ImageUrl == null)
                {
                    ModelState.AddModelError("ImageUrl", "Image photo is required and must be .jpg or .png only.");
                }
            }


            if (ModelState.IsValid && registerView.IsTechnician)
            {
                ApplicationUser user = new ApplicationUser();
                user.Address = registerView.Address;
                user.FirstName = registerView.FirstName;
                user.LastName = registerView.LastName;
                user.Email = registerView.Email;
                user.PhoneNumber = registerView.PhoneNumber;
                user.UserName = registerView.Email;

                IdentityResult identityResult = await UserManager.CreateAsync(user, registerView.Password);
                if (identityResult.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "Technician");

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("FirstName", registerView.FirstName));
                    await SignInManager.SignInWithClaimsAsync(user, false, claims);

                    if (registerView.ImageUrl != null && registerView.ImageUrl.Length > 0)
                    {
                        // Validate the file type (optional)
                        var fileExtension = Path.GetExtension(registerView.ImageUrl.FileName);
                        if (fileExtension != ".jpg" && fileExtension != ".png")
                        {
                            ModelState.AddModelError("ImageUrl", "Only .jpg and .png files are allowed.");
                            return View(registerView);
                        }

                        // Generate a unique filename
                        var fileName = Guid.NewGuid().ToString() + fileExtension;
                        var filePath = Path.Combine("wwwroot/images", fileName); // Adjust path as necessary

                        // Save the file to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await registerView.ImageUrl.CopyToAsync(stream);
                        }

                        // Store the URL in the database
                        var userid = await technicianService.CreateTechnician(registerView.FirstName, registerView.LastName, registerView.Email,
                                                                 registerView.PhoneNumber, registerView.City, registerView.Address,
                                                                 registerView.NationalID, registerView.JobTitle, "/images/" + fileName, registerView.Bio);
                        if (userid == 0)
                        {
                            await UserManager.DeleteAsync(user);
                            ModelState.AddModelError("", "Error");
                            return View(registerView);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View("Register", registerView);
        }

        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Login([FromQuery] string? URL)
        {
            ViewData["ReturnUrl"] = URL;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userLogin = await UserManager.FindByEmailAsync(loginViewModel.Email);

                if (userLogin != null)
                {
                    bool isPasswordCorrect = await UserManager.CheckPasswordAsync(userLogin, loginViewModel.Password);

                    if (isPasswordCorrect)
                    {
                        var claims = new List<Claim>
                        {
                           new Claim("FirstName", userLogin.FirstName)
                        };

                        await SignInManager.SignInWithClaimsAsync(userLogin, loginViewModel.RememberMe, claims);
                        if (string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ModelState.AddModelError("", "Username or password is incorrect.");
            }
            ViewData["ReturnUrl"] = ReturnUrl;
            return View(loginViewModel);
        }






        [Route("Account/technicalProfile/{TechId:int}")]
        public IActionResult technicalProfile([FromRoute] int TechId)
        {


            var TechDb = context.Technician.FirstOrDefault(x => x.Id == TechId);
            var servicesDb = context.Service
                .Where(s => s.TechnicianService.Any(ts => ts.TechnicianId == TechId)).ToList();


            //var reviews = context.Review.Where(r => r.TechnicianId == TechId).ToList();
            //int reviewsCount = reviews.Any() ? reviews.Count : 0;
            //decimal averageRating = (decimal)((reviewsCount > 0) ? Math.Round(reviews.Average(r => r.Rating), 2) : 0);

            var services = new List<ServiceForProviderViewModel>();
            foreach (var service in servicesDb)
            {
                decimal ServicePrice = (context.TechnicianService.FirstOrDefault(ts => ts.ServiceId == service.Id) != null)
                    ? Convert.ToDecimal(context.TechnicianService.FirstOrDefault(ts=>ts.ServiceId == service.Id).Price)
                    : 0;



                services.Add(new ServiceForProviderViewModel
                {
                    ServiceName = service.Name,
                    ServiceDescription = service.Description,
                    ServicePrice = ServicePrice,
                    ServiceRating = 4,
                    ServiceReviewsCount = 455,
                });
            }


            var Aboellil = new ProviderPortfolioViewModel
            {
                ProviderName = TechDb.FirstName + " " + TechDb.LastName,
                ProviderImageUrl = TechDb.ImageUrl,
                ProviderRating = 5,
                TotalReviews = 45,
                TotalTasksCompleted = 483, // total tasks
                ProviderBio = TechDb.Bio,
                Skills = servicesDb.Select(x => x.Name).ToList(),
                Services = services
            };



            var model = new ProviderPortfolioViewModel
            {
                ProviderName = "Carlos S.",
                ProviderImageUrl = "https://images.pexels.com/photos/1499327/pexels-photo-1499327.jpeg?auto=compress&cs=tinysrgb&w=600",
                ProviderRating = 4.9m,
                TotalReviews = 305,
                TotalTasksCompleted = 483,
                ProviderBio = "I am thrilled at the prospect of assisting you with your needs, and I extend my sincere gratitude for considering my services.",
                Skills = new List<string> { "Cleaning", "Electrical Help", "Full Service Moving" },
                Services = new List<ServiceForProviderViewModel>
                {
                    new ServiceForProviderViewModel
                    {
                        ServiceName = "Cleaning",
                        ServicePrice = 72.37m,
                        ServiceRating = 4.9m,
                        ServiceReviewsCount = 123,
                        ServiceDescription = "Beyond technical skills, I pride myself on professionalism and commitment to customer satisfaction."
                    },
                    new ServiceForProviderViewModel
                    {
                        ServiceName = "Electrical Help",
                        ServicePrice = 60.31m,
                        ServiceRating = 5.0m,
                        ServiceReviewsCount = 2,
                        ServiceDescription = "I have a vast experience in multiple construction trades and know exactly how to help you."
                    }
                }
            };
            if (TechDb == null)
            {
                return NotFound($"tech {TechId} not found");
            }


            return View(Aboellil);
        }



        // Edit profile
       
        




    }
}
