using ERestaurant.Domain.Domain;
using ERestaurant.Domain.DTO;
using ERestaurant.Repository.Implementation;
using ERestaurant.Repository.Interface;
using ERestaurant.Service.Interface;
using EShop.Service.Interface;
using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<MenuItem> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ItemInShoppingCart> _productInShoppingCartRepository;
        private readonly IRepository<ItemInOrder> _productInOrderRepository;
        private readonly IEmailService _emailService;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<MenuItem> productRepository, IRepository<Order> orderRepository, IRepository<ItemInShoppingCart> productInShoppingCartRepository, IRepository<ItemInOrder> productInOrderRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _productInOrderRepository = productInOrderRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public bool AddToShoppingConfirmed(ItemInShoppingCart model, string userId)
        {
            if (userId==null)
            {
                return false;
            }

            var loggedInUser = _userRepository.Get(userId);
            var shoppingCart = loggedInUser?.ShoppingCart;

            if(shoppingCart.ItemInShoppingCarts == null)
            {
                shoppingCart.ItemInShoppingCarts = new List<ItemInShoppingCart>();
            }

            shoppingCart.ItemInShoppingCarts.Add(model);
            _shoppingCartRepository.Update(shoppingCart);
            return true;


        }

        public bool deleteProductFromShoppingCart(string userId, Guid productId)
        {
            if (userId == null)
                return false;

            var loggedInUser = _userRepository.Get(userId);
            var userShoppingCart = loggedInUser?.ShoppingCart;
            var product = userShoppingCart?.ItemInShoppingCarts.Where(x => x.MenuItemId == productId).FirstOrDefault();

            userShoppingCart.ItemInShoppingCarts.Remove(product);
            _shoppingCartRepository.Update(userShoppingCart);
            return true;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);
            var userShoppingCart = loggedInUser?.ShoppingCart;
            var allProduct = userShoppingCart?.ItemInShoppingCarts?.ToList();

            var totalPrice = allProduct.Select(x => (x.MenuItem.Price * x.Quantity)).Sum();

            ShoppingCartDto dto = new ShoppingCartDto
            {
                Products = allProduct,
                TotalPrice = totalPrice
            };
            return dto;
        }

        public bool order(string userId)
        {
            if (userId == null)
                return false;

            var loggedInUser = _userRepository.Get(userId);

            var userShoppingCart = loggedInUser?.ShoppingCart;
            EmailMessage message = new EmailMessage();
            message.Subject = "Successfull order";
            message.MailTo = loggedInUser.Email;
            Order order = new Order
            {
                Id = Guid.NewGuid(),
                userId = userId,
                User = loggedInUser
            };

            List<ItemInOrder> productsInOrder = new List<ItemInOrder>();

            var rez = userShoppingCart.ItemInShoppingCarts.Select(
                z => new ItemInOrder
                {
                    Id = Guid.NewGuid(),
                    MenuItemId = z.MenuItem.Id,
                    MenuItem = z.MenuItem,
                    OrderId = order.Id,
                    Order = order,
                    Quantity = z.Quantity
                }).ToList();


            StringBuilder sb = new StringBuilder();

            var totalPrice = 0.0;

            sb.AppendLine("Your order is completed. The order conatins: ");

            for (int i = 1; i <= rez.Count(); i++)
            {
                var currentItem = rez[i - 1];
                totalPrice += (int)currentItem.Quantity * currentItem.MenuItem.Price;
                sb.AppendLine(i.ToString() + ". " + currentItem.MenuItem.Name + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.MenuItem.Price);
            }

            sb.AppendLine("Total price for your order: " + totalPrice.ToString());
            message.Content = sb.ToString();

            productsInOrder.AddRange(rez);

            foreach (var product in productsInOrder)
            {
                _productInOrderRepository.Insert(product);
            }

            loggedInUser.ShoppingCart.ItemInShoppingCarts.Clear();
            _userRepository.Update(loggedInUser);
            this._emailService.SendEmailAsync(message);

            return true;
        }
    }
}
