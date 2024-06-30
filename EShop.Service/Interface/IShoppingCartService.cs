using ERestaurant.Domain.Domain;
using ERestaurant.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Interface
{
    public interface IShoppingCartService
    {
        bool AddToShoppingConfirmed(ItemInShoppingCart model, string userId);
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteProductFromShoppingCart(string userId, Guid productId);
        bool order(string userId);
    }
}
