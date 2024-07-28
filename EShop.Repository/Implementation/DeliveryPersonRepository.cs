using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Domain;
using ERestaurant.Repository.Interface;

namespace ERestaurant.Repository.Implementation
{
    public class DeliveryPersonRepository : IDeliveryPersonRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<DeliveryPerson> _dbSet;

        public DeliveryPersonRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<DeliveryPerson>();
        }

        public async Task<DeliveryPerson> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<DeliveryPerson>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(DeliveryPerson entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DeliveryPerson entity)
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

        Task<IEnumerable<DeliveryPerson>> IDeliveryPersonRepository.GetDeliveryPersonsByRestaurantAsync(Guid restaurantId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<DeliveryPerson>> IDeliveryPersonRepository.GetDeliveryPersonsByOrderAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        Task<DeliveryPerson?> IDeliveryPersonRepository.GetDeliveryPersonByPhoneAsync(string phone)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<DeliveryPerson>> IDeliveryPersonRepository.GetDeliveryPersonsByVehicleAsync(string vehicle)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DeliveryPerson> IRepository<DeliveryPerson>.GetAll()
        {
            throw new NotImplementedException();
        }

        DeliveryPerson IRepository<DeliveryPerson>.Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        void IRepository<DeliveryPerson>.Insert(DeliveryPerson entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<DeliveryPerson>.Update(DeliveryPerson entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<DeliveryPerson>.Delete(DeliveryPerson entity)
        {
            throw new NotImplementedException();
        }
    }
}
