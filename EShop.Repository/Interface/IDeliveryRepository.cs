using Restaurant.Domain.Domain;
using Restaurant.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Repository.Interface
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {
        // Example method to get deliveries by order ID
        Task<IEnumerable<Delivery>> GetDeliveriesByOrderIdAsync(Guid orderId);

        // Example method to get deliveries by status
        Task<IEnumerable<Delivery>> GetDeliveriesByStatusAsync(DeliveryStatus status);

        // Example method to get deliveries assigned to a specific delivery person
        Task<IEnumerable<Delivery>> GetDeliveriesByDeliveryPersonIdAsync(Guid deliveryPersonId);
        Task<IEnumerable<Delivery>> GetAllAsync();
        Task<Delivery> GetByIdAsync(Guid id);
        Task UpdateAsync(Delivery delivery);
    }
}
