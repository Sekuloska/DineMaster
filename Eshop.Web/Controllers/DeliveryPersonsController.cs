using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERestaurant.Repository;
using Restaurant.Domain.Domain;

namespace ERestaurant.Web.Controllers
{
    public class DeliveryPersonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryPersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryPersons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeliveryPeople.Include(d => d.Restaurant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeliveryPersons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPerson = await _context.DeliveryPeople
                .Include(d => d.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryPerson == null)
            {
                return NotFound();
            }

            return View(deliveryPerson);
        }

        // GET: DeliveryPersons/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: DeliveryPersons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantId,Name,Phone,Vehicle,Id")] DeliveryPerson deliveryPerson)
        {
            if (ModelState.IsValid)
            {
                deliveryPerson.Id = Guid.NewGuid();
                _context.Add(deliveryPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", deliveryPerson.RestaurantId);
            return View(deliveryPerson);
        }

        // GET: DeliveryPersons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPerson = await _context.DeliveryPeople.FindAsync(id);
            if (deliveryPerson == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", deliveryPerson.RestaurantId);
            return View(deliveryPerson);
        }

        // POST: DeliveryPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RestaurantId,Name,Phone,Vehicle,Id")] DeliveryPerson deliveryPerson)
        {
            if (id != deliveryPerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryPersonExists(deliveryPerson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", deliveryPerson.RestaurantId);
            return View(deliveryPerson);
        }

        // GET: DeliveryPersons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPerson = await _context.DeliveryPeople
                .Include(d => d.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryPerson == null)
            {
                return NotFound();
            }

            return View(deliveryPerson);
        }

        // POST: DeliveryPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deliveryPerson = await _context.DeliveryPeople.FindAsync(id);
            if (deliveryPerson != null)
            {
                _context.DeliveryPeople.Remove(deliveryPerson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryPersonExists(Guid id)
        {
            return _context.DeliveryPeople.Any(e => e.Id == id);
        }
    }
}
