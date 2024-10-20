using System.ComponentModel.DataAnnotations;

namespace Services_Booking_System.View_Models
{
    public class ProviderPortfolioViewModel
    {
        public string ProviderName { get; set; }
        public string ProviderImageUrl { get; set; }
        public decimal ProviderRating { get; set; }
        public int TotalReviews { get; set; }
        public int TotalTasksCompleted { get; set; }
        public string ProviderBio { get; set; }
        public List<string> Skills { get; set; }
        public List<ServiceForProviderViewModel> Services { get; set; }
    }

    public class ServiceForProviderViewModel
    {
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal ServiceRating { get; set; }
        public int ServiceReviewsCount { get; set; }
        public string ServiceDescription { get; set; }
    }

  

}
