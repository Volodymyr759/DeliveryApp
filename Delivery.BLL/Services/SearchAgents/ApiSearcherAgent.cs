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
    /// API-клієнт поштового оператора
    /// </summary>
    public class ApiSearcherAgent : ISearchAgent
    {
        private readonly string name = "New Post";

        private readonly string apiKey;

        /// <summary>
        /// Ключ доступу до Api-сервісу, надається провайдером сервісу
        /// </summary>
        /// <param name="apiKey">Ключ доступу</param>
        public ApiSearcherAgent(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Повертає назву пошукового агента, якого реалізовано в системі Delivery
        /// </summary>
        /// <returns>Назва пошукового агента</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Шукає поштове відправлення по номеру і повертає актуальний статус з інформаційно системи поштового оператора
        /// </summary>
        /// <param name="number">Номер відправлення</param>
        /// <returns>Актуальний статус відправлення</returns>
        public async Task<string> GetStatus(string number)
        {
            var apiInvoice = (await GetInvoices()).Where(i => i.IntDocNumber == number).FirstOrDefault();

            return (apiInvoice == null) ? "" : apiInvoice.StateName;
        }

        /// <summary>
        /// Шукає поштове відправлення по номеру і повертає Dto модель, включаючи назву оператора
        /// </summary>
        /// <param name="number">Номер відправлення в інформаційній системі поштового оператора</param>
        /// <returns>Екземпляр Dto моделі відправлення</returns>
        public InvoiceDto SearchByNumber(string number)
        {
            throw new NotImplementedException();
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
                    //отримали обєкт класу ApiInvoiceResultModel одразу з вкладеним обєктом ApiInvoiceModel
                    result = await response.Content.ReadAsAsync<InvoiceResultModel>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result.Data;//invoices = result.data -конвертуєм у вкладену модель
        }
    }
}
