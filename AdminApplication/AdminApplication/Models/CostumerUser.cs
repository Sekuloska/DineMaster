using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.VisualBasic;

namespace AdminApplication.Models
{
    public class CostumerUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
      //  public virtual ShoppingCart? ShoppingCart { get; set; }
       // public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
