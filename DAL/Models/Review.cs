
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DAL.Models;

public class Review : ModelsBase
{
    public int Rating { get; set; }
    public string FeedBack { get; set; }
    public int UserId { get; set; }
    public int TechnicianId { get; set; }
    public User User { get; set; }
    public Technician Technician { get; set; }
}
