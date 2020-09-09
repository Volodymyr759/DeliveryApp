using AutoMapper;
using Delivery.BLL.Services;
using Delivery.Web.Models;
using System;
using System.Web.Mvc;

namespace Delivery.Web.Controllers
{
    /// <summary>
    /// Main controller, start page
    /// </summary>
    public class MainController : Controller
    {

        #region Private Properties

        private IDeliveryMessage deliveryMessage;

        private readonly IPostOperatorService postOperatorsService;

        private readonly IInvoicesService invoicesService;

        private readonly IMapper mapper;

        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="deliveryMessage">Instance of the users message</param>
        /// <param name="postOperatorsService">Object of the PostOperators service</param>
        /// <param name="invoicesService">>Object of the Invoices service</param>
        /// <param name="mapper">Object  map</param>
        public MainController(IDeliveryMessage deliveryMessage, 
                              IPostOperatorService postOperatorsService, 
                              IInvoicesService invoicesService,
                              IMapper mapper)
        {
            this.deliveryMessage = deliveryMessage;
            this.postOperatorsService = postOperatorsService;
            this.invoicesService = invoicesService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns main page with the form for search posting by the number and the list of post operators 
        /// </summary>
        /// <returns>Main page</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                var mainIndexViewModel = new MainIndexViewModel
                {
                    Number = "",
                    PostOperators = postOperatorsService.GetAll()
                };

                return View(mainIndexViewModel);
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Index";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Searchs posting in accounting systems of post operators
        /// </summary>
        /// <param name="model">View model of the posting</param>
        /// <returns>Page with posting info</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(MainIndexViewModel model)
        {
            try
            {
                if (model.Number.Length < 6 || model.Number.Length > 30) throw new Exception("Введіть номер від 6 до 30 символів.");
                var invoiceDto = invoicesService.SearchByNumber(model.Number);
                if (invoiceDto == null) throw new Exception("Відправлення не знайдено.");

                return View("Details", mapper.Map<InvoiceViewModel>(invoiceDto));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Index";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

    }
}