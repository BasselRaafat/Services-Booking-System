using BookingService.DAL.Models;

namespace Services_Booking_System.View_Models
{
    public class ServiceDetailsViewModel
    {
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        public List<ServiceProviderViewModel> ServiceProviders { get; set; }
    }

    public class ServiceProviderViewModel
    {
        public string TechnicianName { get; set; }
        public string TechnicianDescription { get; set; }
        public string TechnicianAddress {  get; set; }
        public string TechnicianImageUrl { get; set; }
        public decimal TechnicianPrice { get; set; }
        public decimal TechnicianReview {  get; set; }
        public int ReviewsNum { get; set; }
    }
}
