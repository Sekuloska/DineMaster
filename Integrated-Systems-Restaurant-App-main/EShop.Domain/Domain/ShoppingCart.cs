using EShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.Domain
{
    public class ShoppingCart:BaseEntity
    {
        public string? OwnerId { get; set; }
        public CostumerUser? Owner { get; set; }
        public virtual ICollection<ItemInShoppingCart>? ItemInShoppingCarts { get; set; }
    }
}
