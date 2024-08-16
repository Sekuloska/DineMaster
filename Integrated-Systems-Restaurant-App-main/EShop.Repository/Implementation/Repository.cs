using ERestaurant.Domain.Domain;
using ERestaurant.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public T Get(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (typeof(T) == typeof(Order))
            {
                // Fetch the order with related ItemInOrders and MenuItems
                var order = entities
                    .OfType<Order>()
                    .Include(o => o.ItemInOrders)
                        .ThenInclude(io => io.MenuItem) // Include related MenuItem for each ItemInOrder
                    .SingleOrDefault(o => o.Id == id);

                return (T) (object) order; // Cast to T
            }

            // Handle cases for other types if necessary
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(DeliveryPerson))
            {
                return entities.Include("Restaurant").AsEnumerable();
            }
            if (typeof(T) == typeof(Order))
            {
                return entities.Include("ItemInOrders")
                    .Include("ItemInOrders.MenuItem").AsEnumerable();
            }
            return entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
