using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.Domain
{
    public class ItemInShoppingCart : BaseEntity
    {
        public Guid? MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public Guid? ShoppingCartId { get; set;}
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }

}
