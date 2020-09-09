using System;
using System.Collections.Generic;
using Delivery.BLL.DTO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// API client of the postal operator
    /// </summary>
    public class ApiSearcherAgent : ISearchAgent
    {
        private readonly string name = "Нова Пошта";

        private readonly string apiKey;

        /// <summary>
        /// The access key to the Api service, is provided by the service provider
        /// </summary>
        /// <param name="apiKey">The access key</param>
        public ApiSearcherAgent(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Returns the name of the search agent implemented in the Delivery system
        /// </summary>
        /// <returns>Search agent name</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Searches for a postal item by number and returns the current status from the information system of the postal operator
        /// </summary>
        /// <param name="number">Shipment number</param>
        /// <returns>Current shipment status</returns>
        public async Task<string> GetStatus(string number)
        {
            var apiInvoice = (await GetInvoices()).Where(i => i.IntDocNumber == number).FirstOrDefault();

            return (apiInvoice == null) ? "" : apiInvoice.StateName;
        }

        /// <summary>
        /// Searches for a postal item by number and returns the Dto model, including the name of the operator
        /// </summary>
        /// <param name="number">Shipment number in the information system of the postal operator</param>
        /// <returns>An instance of the Dto shipment model</returns>
        public async Task<InvoiceDto> SearchByNumber(string number)
        {
            InvoiceDto invoiceDto = null;
            var apiInvoice = (await GetInvoices()).Where(i => i.IntDocNumber == number).FirstOrDefault();
            if (apiInvoice != null)
            {
                invoiceDto.PostOperatorName = name;
                invoiceDto.Number = number;
                invoiceDto.SendDateTime = Convert.ToDateTime(apiInvoice.Datetime);
                invoiceDto.Sender = apiInvoice.SendersPhone;
                invoiceDto.SenderAddress = apiInvoice.CitySenderDescription + ": " + apiInvoice.SenderAddressDescription;
                invoiceDto.Recipient = apiInvoice.CityRecipientDescription;
                invoiceDto.RecipientAddress = apiInvoice.RecipientAddressDescription;
                invoiceDto.CurrentLocation = "Не вказано";
                invoiceDto.ActualStatus = apiInvoice.StateName;
                invoiceDto.Notes = "Вартість: " + apiInvoice.CostOnSite + " Тип доставки: " + apiInvoice.ServiceType + 
                    " Вага: " + apiInvoice.Weight + " Вид оплати: " + apiInvoice.PaymentMethod + " Платник: " + apiInvoice.PayerType + 
                    " Опис: " + apiInvoice.Description;
            }
            return invoiceDto;
        }

        private async Task<List<ApiInvoicesModel>> GetInvoices()
        {
            HttpClient httpClient = new HttpClient();
            string baseaddress = "https://api.novaposhta.ua/v2.0/json/?format=json";
            httpClient.BaseAddress = new Uri(baseaddress);

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var methodProps = new ApiMethodProperties() { DateTimeFrom = DateTime.Today.AddDays(-6).ToShortDateString(), DateTimeTo = DateTime.Today.ToShortDateString(), GetFullList = "1" };
            var document = new ApiDocument() { ApiKey = apiKey, ModelName = "InternetDocument", CalledMethod = "getDocumentList", MethodProperties = methodProps };

            var result = new InvoiceResultModel();
            try
            {
                using (HttpResponseMessage response = await httpClient.PostAsJsonAsync(baseaddress, document))
                {
                    if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                    //отримали об'єкт класу ApiInvoiceResultModel одразу з вкладеним об'єктом ApiInvoiceModel
                    result = await response.Content.ReadAsAsync<InvoiceResultModel>();
                }
            }
            catch (Exception)
            {
                throw new Exception("Немає доступу до інформаційної системи Нової пошти.");
            }
            return result.Data;//invoices = result.data -конвертуєм у вкладену модель
        }
    }
}
