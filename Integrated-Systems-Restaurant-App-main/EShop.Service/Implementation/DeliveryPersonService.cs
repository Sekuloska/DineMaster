using ERestaurant.Repository.Interface;
using ERestaurant.Service.Interface;
using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Implementation
{
    public class DeliveryPersonService:IDeliveryPersonService
    {
        private readonly IRepository<DeliveryPerson> _deliveryPersonRepository;

        public DeliveryPersonService(IRepository<DeliveryPerson> deliveryPersonRepository)
        {
            _deliveryPersonRepository = deliveryPersonRepository;
        }

        public DeliveryPerson GetDeliveryPersonByIdAsync(Guid id)
        {
            return  _deliveryPersonRepository.Get(id);
        }

        public  List<DeliveryPerson> GetAllDeliveryPersons()
        {
            return  _deliveryPersonRepository.GetAll().ToList();
        }

        public void CreateDeliveryPersonAsync(DeliveryPerson deliveryPerson)
        {
            _deliveryPersonRepository.Insert(deliveryPerson);
        }

        public DeliveryPerson UpdateDeliveryPersonAsync(DeliveryPerson deliveryPerson)
        {
            _deliveryPersonRepository.Update(deliveryPerson);
            return deliveryPerson;
        }

        public DeliveryPerson DeleteDeliveryPersonAsync(Guid id)
        {
            var deliveryPerson =_deliveryPersonRepository.Get(id);
            if (deliveryPerson != null)
            {
                 _deliveryPersonRepository.Delete(deliveryPerson);
            }
            return deliveryPerson;
        }
    }
}
