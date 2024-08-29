namespace AdminApplication.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string userId { get; set; }
        public CostumerUser User { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
