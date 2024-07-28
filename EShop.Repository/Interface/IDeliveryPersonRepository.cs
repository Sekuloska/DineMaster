using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Repository.Interface
{
    public interface IDeliveryPersonRepository : IRepository<DeliveryPerson>
    {
        // Get all delivery persons for a specific restaurant
        Task<IEnumerable<DeliveryPerson>> GetDeliveryPersonsByRestaurantAsync(Guid restaurantId);

        // Get delivery persons assigned to a specific order
        Task<IEnumerable<DeliveryPerson>> GetDeliveryPersonsByOrderAsync(Guid orderId);

        // Get delivery person by phone number (assuming it's unique)
        Task<DeliveryPerson?> GetDeliveryPersonByPhoneAsync(string phone);

        // Get delivery person by vehicle (assuming you want to search by vehicle type)
        Task<IEnumerable<DeliveryPerson>> GetDeliveryPersonsByVehicleAsync(string vehicle);
        Task<IEnumerable<DeliveryPerson>> GetAllAsync();
        Task<DeliveryPerson> GetByIdAsync(Guid id);
        Task AddAsync(DeliveryPerson deliveryPerson);
    }
}
