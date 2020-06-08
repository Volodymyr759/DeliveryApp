using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.BLL.DTO;
using Delivery.BLL.Validators;
using Delivery.DAL.Models;
using Delivery.DAL.Repositories;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Сервіс управління посилками/відправленнями
    /// </summary>
    public class InvoicesService : IInvoicesService
    {
        private readonly string connectionString;

        private readonly IInvoicesRepository invoicesRepository;

        private InvoicesValidator invoicesValidator = new InvoicesValidator();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString">Строка підключення</param>
        /// <param name="invoicesRepository">Об'єкт репозиторію відправлень</param>
        public InvoicesService(string connectionString, IInvoicesRepository invoicesRepository)
        {
            this.connectionString = connectionString;
            this.invoicesRepository = invoicesRepository;
        }

        /// <summary>
        /// Створення нового відправлення користувача
        /// </summary>
        /// <param name="userId">Ідентифікатр користувача</param>
        /// <param name="number">Номер відправлення</param>
        /// <param name="apiKeys">Ключі доступу до реалізованих Api-сервісів</param>
        public void Add(string userId, string number, Dictionary<string, string> apiKeys)
        {
            var invoiceDto = SearchByNumber(number, apiKeys);
            if (invoiceDto != null)
            {
                Invoice invoice = ConvertDtoToInvoice(invoiceDto);
                var results = invoicesValidator.Validate(invoice);
                if (results.IsValid)
                {
                    invoicesRepository.Create(invoice);
                }
                else
                {
                    throw new Exception("Помилка валідації відправлення: " + Environment.NewLine +
                        ValidationResultsHelper.GetValidationErrors(results));
                }
            }
            else
            {
                throw new Exception("Відправлення не зайдено.");
            }
        }

        /// <summary>
        /// Повертає усі створені користувачами відправлення в сервісі Delivery
        /// </summary>
        /// <returns>Список відправлень</returns>
        public IEnumerable<InvoiceDto> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<List<Invoice>, List<InvoiceDto>>()).CreateMapper();
            return mapper.Map<List<InvoiceDto>>(invoicesRepository.GetAll());
        }

        /// <summary>
        /// Повертає відправлення по ідентифікатору
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        /// <returns>Модель Dto відправлення</returns>
        public InvoiceDto GetById(int invoiceId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Invoice, InvoiceDto>()).CreateMapper();
            return mapper.Map<InvoiceDto>(invoicesRepository.GetById(invoiceId));
        }

        /// <summary>
        /// Повертає список відправлень обраного користувача
        /// </summary>
        /// <param name="userId">Ідентифікатр користувача</param>
        /// <returns></returns>
        public IEnumerable<InvoiceDto> GetInvoicesByUserId(string userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<List<Invoice>, List<InvoiceDto>>()).CreateMapper();
            return mapper.Map<List<InvoiceDto>>(invoicesRepository.GetByUserId(userId));
        }

        /// <summary>
        /// Видалення відправлення
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        public void Remove(int invoiceId)
        {
            invoicesRepository.Delete(invoiceId);
        }

        /// <summary>
        /// Видалення відправлень користувача
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        public void RemoveByUser(string userId)
        {
            invoicesRepository.DeleteByUserId(userId);
        }

        /// <summary>
        /// Пошук відправлення по номеру
        /// </summary>
        /// <param name="number">Номер відправлення в інформаційній системі поштового оператора</param>
        /// <param name="apiKeys">Ключі доступу до реалізованих Api-сервісів</param>
        public InvoiceDto SearchByNumber(string number, Dictionary<string, string> apiKeys)
        {
            try
            {
                InvoiceDto invoiceDto = null;
                foreach (var agent in FactoryOfAgents.GetAllAgents(apiKeys))
                {
                    invoiceDto = agent.SearchByNumber(number);
                    if (invoiceDto != null) break;
                }
                return invoiceDto;
            }
            catch (Exception)
            {
                throw new Exception("Помилка доступу до інформаційної системи поштового оператора.");
            }
        }

        /// <summary>
        /// Оновлює статус поштового відправлення
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        /// <param name="apiKeys">Ключі доступу до реалізованих Api-сервісів</param>
        public async Task UpdateStatusAsync(int invoiceId, Dictionary<string, string> apiKeys)
        {
            var invoice = invoicesRepository.GetById(invoiceId);
            if (invoice != null)
            {
                string postOperatorName = invoicesRepository.GetPostOperatorsIdNames()[invoiceId];
                foreach (var agent in FactoryOfAgents.GetAllAgents(apiKeys))
                {
                    if (postOperatorName == agent.GetName())
                    {
                        string status = await agent.GetStatus(invoice.Number);
                        if (status != "")
                        {
                            invoicesRepository.UpdateStatus(invoiceId, status);
                            break;
                        }
                        else
                        {
                            throw new Exception("Відправлення не знайдено.");
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Відправлення не знайдено.");
            }
        }

        #region Helpers

        private Invoice ConvertDtoToInvoice(InvoiceDto invoiceDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<InvoiceDto, Invoice>()).CreateMapper();
            Invoice invoice = mapper.Map<Invoice>(invoiceDto);
            foreach (KeyValuePair<int, string> keyValue in invoicesRepository.GetPostOperatorsIdNames())
            {
                if (invoiceDto.PostOperatorName == keyValue.Value)
                {
                    invoice.PostOperatorId = keyValue.Key;
                    break;
                }
            }

            return invoice;
        }

        #endregion
    }
}
