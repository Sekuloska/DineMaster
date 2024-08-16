using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DTO
{
    public class MenuItemDto
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
       // public Menu Menu { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public string ItemImage { get; set; }
        public virtual ICollection<ItemInOrder>? ItemInOrders { get; set; }
    }
}
