using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DAL.Data.Identity
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
