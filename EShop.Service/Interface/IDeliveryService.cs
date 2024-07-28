using Restaurant.Domain.Domain;
using Restaurant.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Interface
{
    public interface IDeliveryService
    {
        Task<IEnumerable<Delivery>> GetAllDeliveriesAsync();
        Task<Delivery> GetDeliveryByIdAsync(Guid id);
        Task AssignDeliveryPersonAsync(Guid deliveryId, Guid deliveryPersonId);
        Task UpdateDeliveryStatusAsync(Guid deliveryId, DeliveryStatus status);
        Task<byte[]> GenerateInvoicePdfAsync(Guid orderId); // For invoice generation
    }

    public interface IDeliveryPersonService
    {
        Task<IEnumerable<DeliveryPerson>> GetAllDeliveryPersonsAsync();
        Task<DeliveryPerson> GetDeliveryPersonByIdAsync(Guid id);
        Task AddDeliveryPersonAsync(DeliveryPerson deliveryPerson);
    }
}
