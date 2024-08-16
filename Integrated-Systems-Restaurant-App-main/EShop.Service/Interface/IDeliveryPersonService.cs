using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Interface
{
    public interface IDeliveryPersonService
    {
        DeliveryPerson GetDeliveryPersonByIdAsync(Guid id);
        List<DeliveryPerson> GetAllDeliveryPersons();
        void CreateDeliveryPersonAsync(DeliveryPerson deliveryPerson);
        DeliveryPerson UpdateDeliveryPersonAsync(DeliveryPerson deliveryPerson);
        DeliveryPerson DeleteDeliveryPersonAsync(Guid id);
    }
}
