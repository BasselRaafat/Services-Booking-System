using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WEBPage.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }       
        public string LastName { get; set; }
        public string Address { get; set; }
        //public string? NationalID { get; set; }
        //public string? JobTitle { get; set; }
    }
}
