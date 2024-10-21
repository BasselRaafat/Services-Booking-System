using System.ComponentModel.DataAnnotations;

namespace Services_Booking_System.View_Models
{
    public class AddAdminViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters")]
        public string LastName { get; set; }

        // Email property
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        // Password and Confirm Password properties
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters")]
        public string Address { get; set; }

    }
}
