using System.ComponentModel.DataAnnotations;

namespace AdminApplication.Models
{
    public class DeliveryPerson
    {
        public Guid Id { get; set; }

        
        public Guid RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

       
       public string Vehicle { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
