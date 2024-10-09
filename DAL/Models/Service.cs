namespace BookingService.DAL.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BasePrice { get; set; }
        public int ServiceCategoryId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
