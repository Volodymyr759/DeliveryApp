using AutoMapper;
using Delivery.BLL.DTO;
using Delivery.BLL.Services;
using Delivery.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Delivery.Web.Controllers
{
    /// <summary>
    /// Shipment controller
    /// </summary>
    [Authorize]
    public class InvoicesController : Controller
    {
        private IDeliveryMessage deliveryMessage;

        private readonly IInvoicesService invoicesService;

        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="deliveryMessage">Instance of the users message</param>
        /// <param name="invoicesService">Object of the Invoices service</param>
        /// <param name="mapper">Object map</param>
        public InvoicesController(IDeliveryMessage deliveryMessage, IInvoicesService invoicesService, IMapper mapper)
        {
            this.deliveryMessage = deliveryMessage;
            this.invoicesService = invoicesService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns page with list of user's shipments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                string userId = User.Identity.GetUserId();

                return View(mapper.Map<List<InvoiceViewModel>>(invoicesService.GetInvoicesByUserId(userId)));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Відправлення";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Shipment details
        /// </summary>
        /// <param name="id">Shipment Id</param>
        /// <returns>Page of the shipment info</returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                var invoiceDto = invoicesService.GetById(id);
                if (invoiceDto == null) throw new Exception("Відправлення не знайдено.");

                return View("Details", mapper.Map<InvoiceViewModel>(invoiceDto));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Відправлення";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Returns page for create a new shipment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View("Create");
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Відправлення";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Gets the number from the page of creation of the shipment, checks in the list of existing, searches for new in information systems of postal operators and saves in a DB
        /// </summary>
        /// <param name="model">SearchInvoiceViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SearchInvoiceViewModel model)
        {
            try
            {
                invoicesService.Add(User.Identity.GetUserId(), model.Number);

                return View("Create");
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Відправлення";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Returns a page to confirm the deletion of the shipment
        /// </summary>
        /// <param name="id">Shipment Id</param>
        /// <returns>Page to confirm the deletion of the shipment</returns>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Не обрано відправлення для видалення.");
                InvoiceDto invoiceDto = invoicesService.GetById((int)id);
                if (invoiceDto == null) throw new Exception("Відправлення не знайдено.");

                return View(mapper.Map<InvoiceViewModel>(invoiceDto));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Відправлення";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Deletes the shipment from the db
        /// </summary>
        /// <param name="id">Shipment Id</param>
        /// <param name="invoice">View model of the shipment</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, InvoiceViewModel invoice)
        {
            try
            {
                invoicesService.Remove(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Відправлення";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Updates shipment status
        /// </summary>
        /// <param name="id">Shipment Id</param>
        /// <returns>Redirects to page of the shipment info</returns>
        [HttpPost]
        public ActionResult UpdateStatus(int id)
        {
            try
            {
                InvoiceDto invoiceDto = invoicesService.GetById(id);
                if (invoiceDto == null) throw new Exception("Відправлення не знайдено.");

                invoicesService.UpdateStatusAsync(id);

                return RedirectToAction("Details", id);
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Відправлення";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }
    }
}
