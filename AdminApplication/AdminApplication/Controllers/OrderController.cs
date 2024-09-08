using AdminApplication.Models;
using AdminApplication.Models.DTO;
using AdminApplication.Models.ENUM;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdminApplication.Controllers
{
    public class OrderController : Controller
    {

        public OrderController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://dinemaster.azurewebsites.net/api/OrderApi/GetAllOrders";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Order>>().Result;
            ViewBag.DeliveryStatuses = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().ToList();

            return View(data);

        }

        public IActionResult ItemsInOrder(Guid id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://dinemaster.azurewebsites.net/api/OrderApi/GetDetailsForOrder";
            var model = new
            {
                Id = id
            };

            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(URL, content).Result;

                var data = response.Content.ReadAsAsync<Order>().Result;


                return View(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return BadRequest();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid orderId, OrderStatus newStatus)
        {
            HttpClient client = new HttpClient();

            try
            {
                string url = $"https://dinemaster.azurewebsites.net/api/OrderApi/UpdateStatus/{orderId}/{newStatus}";

                HttpResponseMessage response = client.PatchAsync(url, null).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsAsync<Guid>().Result;
                }
                else
                {
                    Console.WriteLine("Error patching the status of an order!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("");

        }

        public FileContentResult CreateInvoice(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://dinemaster.azurewebsites.net/api/OrderApi/GetDetailsForOrder";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);
            document.Content.Replace("{{orderNumber}}", data.Id.ToString());
            document.Content.Replace("{{orderDate}}", data.OrderDate.ToString());
            document.Content.Replace("{{user}}", data.userId);
            document.Content.Replace("{{deliveryAddress}}", data.DeliveryAddress);
            StringBuilder sb = new StringBuilder();
            var total = 0;
            foreach (var item in data.ItemInOrders)
            {
                sb.Append("Продукт " + item.MenuItem.Name + " со количина " + item.Quantity + " и цена " + item.MenuItem.Price + "мкд, ");
                total += (item.Quantity * item.MenuItem.Price);
            }
            document.Content.Replace("{{ProductList}}", sb.ToString());
            document.Content.Replace("{{totalPrice}}", total.ToString() + "мкд");

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");

        }

        [HttpGet]
        public FileContentResult ExportOrders()
        {
            string fileName = "AllOrders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("Orders");

                worksheet.Cell(1, 1).Value = "Order ID";
                worksheet.Cell(1, 2).Value = "Customer ID";

                HttpClient client = new HttpClient();
                string URL = "https://dinemaster.azurewebsites.net/api/OrderApi/GetAllOrders";
                HttpResponseMessage response = client.GetAsync(URL).Result;

                var data = response.Content.ReadAsAsync<List<Order>>().Result;

                for (int i = 0; i < data.Count(); i++)
                {
                    var order = data[i];
                    worksheet.Cell(i + 2, 1).Value = order.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = order.userId;

                    for (int j = 0; j < order.ItemInOrders.Count(); j++)
                    {
                        worksheet.Cell(1, j + 3).Value = "Product" + (j + 1);
                        worksheet.Cell(i + 2, j + 3).Value = order.ItemInOrders.ElementAt(j).MenuItem.Name;

                    }
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }

    }
}
