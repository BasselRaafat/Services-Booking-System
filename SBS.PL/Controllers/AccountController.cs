using BookingService.BLL.Services;
using BookingService.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Security.Claims;
using System.Security.Policy;
using WEBPage.Models;
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

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UserService userService, TechnicianServices technicianService)
        {
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
                user.PasswordHash = registerView.Password;
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
                user.PasswordHash = registerView.Password;
                user.Email = registerView.Email;
                user.PhoneNumber = registerView.PhoneNumber;
                user.UserName = registerView.Email;

                IdentityResult identityResult = await UserManager.CreateAsync(user);
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
                        var userid=await technicianService.CreateTechnician(registerView.FirstName, registerView.LastName, registerView.Email,
                                                                 registerView.PhoneNumber, registerView.City, registerView.Address,
                                                                 registerView.NationalID, registerView.JobTitle, "/images/" + fileName,registerView.Bio);
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
        public async Task<IActionResult> Login(LoginViewModel loginViewModel,string? ReturnUrl=null)
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

    }
}
