using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Domain;
using EShop.Domain.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using ERestaurant.Repository;
using Restaurant.Domain.Enum;

namespace Restaurant.Web.Controllers
{
    public class UserOrdersController : Controller
    {
        private readonly UserManager<CostumerUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserOrdersController(UserManager<CostumerUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: UserOrders
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var orders = await _context.Orders
                                        .Include(o => o.Restaurant)
                                        .Include(o => o.User)
                                        .Include(o => o.Deliveries)
                                        .Where(o => o.userId == user.Id)
                                        .ToListAsync();

            return View(orders);
        }

        // GET: UserOrders/Details/{id}
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                                       .Include(o => o.Restaurant)
                                       .Include(o => o.User)
                                       .Include(o => o.Deliveries)
                                       .ThenInclude(d => d.DeliveryPerson)
                                       .SingleOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
