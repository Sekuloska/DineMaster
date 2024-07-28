using ERestaurant.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Domain;
using Restaurant.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Repository.Implementation
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Delivery> _dbSet;

        public DeliveryRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Delivery>();
        }

        public async Task<Delivery> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(Delivery entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Delivery entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Delivery>> GetDeliveriesByOrderIdAsync(Guid orderId)
        {
            return await _dbSet.Where(d => d.OrderId == orderId).ToListAsync();
        }

        public async Task<IEnumerable<Delivery>> GetDeliveriesByStatusAsync(DeliveryStatus status)
        {
            return await _dbSet.Where(d => d.DeliveryStatus == status).ToListAsync();
        }

        public async Task<IEnumerable<Delivery>> GetDeliveriesByDeliveryPersonIdAsync(Guid deliveryPersonId)
        {
            return await _dbSet.Where(d => d.DeliveryPersonId == deliveryPersonId).ToListAsync();
        }

        IEnumerable<Delivery> IRepository<Delivery>.GetAll()
        {
            throw new NotImplementedException();
        }

        Delivery IRepository<Delivery>.Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Delivery>.Insert(Delivery entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Delivery>.Update(Delivery entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Delivery>.Delete(Delivery entity)
        {
            throw new NotImplementedException();
        }
    }
}
