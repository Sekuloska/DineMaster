using ERestaurant.Repository.Interface;
using ERestaurant.Service.Interface;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfSharp.Pdf;
using Restaurant.Domain.Domain;
using Restaurant.Domain.Enum;
using System.Reflection.Metadata;
using Document = iTextSharp.text.Document;



namespace ERestaurant.Service.Implementation
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IRepository<Delivery> _deliveryRepository;
        private readonly IRepository<DeliveryPerson> _deliveryPersonRepository;

        public DeliveryService(IRepository<Delivery> deliveryRepository, IRepository<DeliveryPerson> deliveryPersonRepository)
        {
            _deliveryRepository = deliveryRepository;
            _deliveryPersonRepository = deliveryPersonRepository;
        }

        /*
        
        public async Task<IEnumerable<Delivery>> GetAllDeliveriesAsync()
        {
            return null;
           // return await _deliveryRepository.GetAllAsync();
        }

        public async Task<Delivery> GetDeliveryByIdAsync(Guid id)
        {
            return null;
           // return await _deliveryRepository.GetByIdAsync(id);
        }

        public async Task AssignDeliveryPersonAsync(Guid deliveryId, Guid deliveryPersonId)
        {
            return null;
           // var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
           // delivery.DeliveryPersonId = deliveryPersonId;
           // await _deliveryRepository.UpdateAsync(delivery);
        }

        public async Task UpdateDeliveryStatusAsync(Guid deliveryId, DeliveryStatus status)
        {
            return null;
            //var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
           // delivery.DeliveryStatus = status;
            // _deliveryRepository.UpdateAsync(delivery);
        }

        public async Task<byte[]> GenerateInvoicePdfAsync(Guid orderId)
        {
            // Retrieve the order details
            var order = await _deliveryRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            // Create a memory stream to hold the PDF data
            using (var memoryStream = new MemoryStream())
            {
                // Create a new PDF document
                using (var document = new Document(PageSize.A4))
                {
                    // Create a PdfWriter instance to write to the memory stream
                    using (var writer = PdfWriter.GetInstance(document, memoryStream))
                    {
                        // Open the document for writing
                        document.Open();

                        // Add invoice header
                        document.Add(new Paragraph("Invoice")
                        {
                            Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20)
                        });

                        // Add order details
                        document.Add(new Paragraph($"Order ID: {order.Id}"));
                        document.Add(new Paragraph($"Order Date: {order.DeliveryDate}"));
                        document.Add(new Paragraph($"Total Amount: {order.Order.TotalAmount}"));

                        // Add user details
                        document.Add(new Paragraph($"User ID: {order.Order.User.Id}"));
                        document.Add(new Paragraph($"Delivery Address: {order.Order.DeliveryAddress}"));

                        // Add items in the order
                        document.Add(new Paragraph("Items:"));
                        foreach (var item in order.Order.ItemInOrders)
                        {
                            document.Add(new Paragraph($"- {item.Order.ItemInOrders} x {item.Quantity}"));
                        }

                        // Close the document
                        document.Close();
                    }
                }

                // Return the PDF byte array
                return memoryStream.ToArray();
            }
        }

        async Task<IEnumerable<Delivery>> IDeliveryService.GetAllDeliveriesAsync()
        {
            return await _deliveryRepository.GetAllAsync();
        }

        async Task<Delivery> IDeliveryService.GetDeliveryByIdAsync(Guid id)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);
            if (delivery == null)
            {
                throw new KeyNotFoundException("Delivery not found.");
            }
            return delivery;
        }

        async Task IDeliveryService.UpdateDeliveryStatusAsync(Guid deliveryId, DeliveryStatus status)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
            if (delivery == null)
            {
                throw new KeyNotFoundException("Delivery not found.");
            }

            // Updates the delivery status
            delivery.DeliveryStatus = status;

            // Updates the delivery in the repository
            await _deliveryRepository.UpdateAsync(delivery);
        }

        Task IDeliveryService.AssignDeliveryPersonAsync(Guid deliveryId, Guid deliveryPersonId)
        {
            throw new NotImplementedException();
        }

        Task<byte[]> IDeliveryService.GenerateInvoicePdfAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }

    public class DeliveryPersonService : IDeliveryPersonService
    {
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;

        public DeliveryPersonService(IDeliveryPersonRepository deliveryPersonRepository)
        {
            _deliveryPersonRepository = deliveryPersonRepository;
        }

        public async Task<IEnumerable<DeliveryPerson>> GetAllDeliveryPersonsAsync()
        {
            return await _deliveryPersonRepository.GetAllAsync();
        }

        public async Task<DeliveryPerson> GetDeliveryPersonByIdAsync(Guid id)
        {
            return await _deliveryPersonRepository.GetByIdAsync(id);
        }

        public async Task AddDeliveryPersonAsync(DeliveryPerson deliveryPerson)
        {
            await _deliveryPersonRepository.AddAsync(deliveryPerson);
        }
        */
        
    }
}
