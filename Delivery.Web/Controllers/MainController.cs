using AutoMapper;
using Delivery.BLL.DTO;
using Delivery.BLL.Services;
using Delivery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Delivery.Web.Controllers
{
    /// <summary>
    /// Головний конроллер, стартова сторінка
    /// </summary>
    public class MainController : Controller
    {

        #region Private Properties

        private IDeliveryMessage deliveryMessage;

        private readonly IPostOperatorService postOperatorsService;

        private readonly IInvoicesService invoicesService;

        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="deliveryMessage">Екзепляр повідолення користувача</param>
        /// <param name="postOperatorsService">Об'єкт сервісу поштових операторів</param>
        /// <param name="invoicesService">Об'єкт сервісу відправлень</param>
        public MainController(IDeliveryMessage deliveryMessage, IPostOperatorService postOperatorsService, IInvoicesService invoicesService)
        {
            this.deliveryMessage = deliveryMessage;
            this.postOperatorsService = postOperatorsService;
            this.invoicesService = invoicesService;
        }

        /// <summary>
        /// Повертає головну сторінку з формою пошуку відправлення по номеру 
        /// і списком поштових операторів, реалізованих в сервісі Delivery
        /// </summary>
        /// <returns>Головна сторінка</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperatorDto, PostOperatorViewModel>()).CreateMapper();
                List<PostOperatorViewModel> postOperatorViewModels = mapper
                    .Map<List<PostOperatorViewModel>>((List<PostOperatorDto>)postOperatorsService.GetAll());

                return View(postOperatorViewModels);
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Index";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Шукає відправлення в інформаційних системах поштових операторів по номеру і повертає сторінку  
        /// з інформацією про відправлення або повідомлення про відсутність результату пошуку
        /// </summary>
        /// <param name="model">Модель пошуку поштового відправлення</param>
        /// <returns>Сторінка з інформацією про відправлення або повідомлення про відсутність даних</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SearchInvoiceByNumber(SearchInvoiceViewModel model)
        {
            try
            {
                if (model.Number.Length < 6 || model.Number.Length > 30)
                    throw new Exception("Введіть номер від 6 до 30 символів.");
                var invoiceDto = invoicesService.SearchByNumber(model.Number);
                if (invoiceDto == null) throw new Exception("Відправлення не знайдено.");

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<InvoiceDto, InvoiceViewModel>()
                    .ForMember("Notes", opt => opt.MapFrom(dto => dto.PostOperatorName + Environment.NewLine +
                                  dto.Sender + Environment.NewLine +
                                  dto.SenderAddress + Environment.NewLine +
                                  dto.Recipient + Environment.NewLine +
                                  dto.RecipientAddress + Environment.NewLine))).CreateMapper();

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