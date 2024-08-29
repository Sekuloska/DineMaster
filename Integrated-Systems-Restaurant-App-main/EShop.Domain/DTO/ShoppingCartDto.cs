using ERestaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<ItemInShoppingCart>? Products { get; set; }
        public double TotalPrice { get; set; }
    }
}
