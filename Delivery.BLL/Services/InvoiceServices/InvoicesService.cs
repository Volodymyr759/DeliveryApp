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
    /// Shipment Management Service
    /// </summary>
    public class InvoicesService : IInvoicesService
    {
        private readonly string connectionString;

        private readonly IInvoicesRepository invoicesRepository;

        private readonly Dictionary<string, string> apiKeys;

        private InvoicesValidator invoicesValidator = new InvoicesValidator();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="invoicesRepository">Shipment repository object</param>
        /// <param name="apiKeys">Access keys to implemented Api-services</param>
        public InvoicesService(string connectionString, IInvoicesRepository invoicesRepository, Dictionary<string, string> apiKeys)
        {
            this.connectionString = connectionString;
            this.invoicesRepository = invoicesRepository;
            this.apiKeys = apiKeys;
        }

        /// <summary>
        /// Create a new shipment
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="number">Shipment number</param>
        public async Task Add(string userId, string number)
        {
            var invoiceDto = await SearchByNumber(number);
            if (invoiceDto != null)
            {
                int postOperatorId = 0;
                foreach (KeyValuePair<int, string> idName in invoicesRepository.GetPostOperatorsIdNames())
                {
                    if (idName.Value == invoiceDto.PostOperatorName)
                    {
                        postOperatorId = idName.Key;
                        break;
                    }
                }

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<InvoiceDto, Invoice>()).CreateMapper();
                Invoice invoice = mapper.Map<Invoice>(invoiceDto);
                invoice.AccountUserId = userId;
                invoice.PostOperatorId = postOperatorId;

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
                throw new Exception("Відправлення не знайдено.");
            }
        }

        /// <summary>
        /// Returns all user-generated shipments in the Delivery service
        /// </summary>
        /// <returns>List of shipments</returns>
        public IEnumerable<InvoiceDto> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Invoice, InvoiceDto>()).CreateMapper();

            List<InvoiceDto> invoiceDtos = new List<InvoiceDto>();
            foreach (var invoice in invoicesRepository.GetAll())
            {
                InvoiceDto invoiceDto = mapper.Map<InvoiceDto>(invoice);
                invoiceDto.PostOperatorName = invoicesRepository.GetPostOperatorsIdNames()[invoice.PostOperatorId];
                invoiceDtos.Add(invoiceDto);
            }

            return invoiceDtos;
        }

        /// <summary>
        /// Returns the shipment by Id
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        /// <returns>Shipment Dto model</returns>
        public InvoiceDto GetById(int invoiceId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Invoice, InvoiceDto>()).CreateMapper();

            Invoice invoice = (Invoice)invoicesRepository.GetById(invoiceId);
            InvoiceDto invoiceDto = mapper.Map<InvoiceDto>(invoice);
            invoiceDto.PostOperatorName = invoicesRepository.GetPostOperatorsIdNames()[invoice.PostOperatorId];

            return invoiceDto;
        }

        /// <summary>
        /// Returns the shipment list of the selected user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public IEnumerable<InvoiceDto> GetInvoicesByUserId(string userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Invoice, InvoiceDto>()).CreateMapper();

            List<InvoiceDto> invoiceDtos = new List<InvoiceDto>();
            foreach (var invoice in invoicesRepository.GetByUserId(userId))
            {
                InvoiceDto invoiceDto = mapper.Map<InvoiceDto>(invoice);
                invoiceDto.PostOperatorName = invoicesRepository.GetPostOperatorsIdNames()[invoice.PostOperatorId];
                invoiceDtos.Add(invoiceDto);
            }

            return invoiceDtos;
        }

        /// <summary>
        /// Deleting a shipment
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        public void Remove(int invoiceId) => invoicesRepository.Delete(invoiceId);

        /// <summary>
        /// Delete user shipments
        /// </summary>
        /// <param name="userId">User Id</param>
        public void RemoveByUser(string userId) => invoicesRepository.DeleteByUserId(userId);

        /// <summary>
        /// Search for a shipment by number
        /// </summary>
        /// <param name="number">Shipment number in the information system of the postal operator</param>
        /// <returns>Shipment Dto model</returns>
        public async Task<InvoiceDto> SearchByNumber(string number)
        {
            try
            {
                InvoiceDto invoiceDto = null;
                foreach (var agent in FactoryOfAgents.GetAllAgents(apiKeys))
                {
                    invoiceDto = await agent.SearchByNumber(number);

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
        /// Updates the status of the shipment
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        public async Task UpdateStatusAsync(int invoiceId)
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
    }
}
