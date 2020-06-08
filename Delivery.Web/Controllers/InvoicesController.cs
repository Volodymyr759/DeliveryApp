using AutoMapper;
using Delivery.BLL.DTO;
using Delivery.BLL.Services;
using Delivery.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Delivery.Web.Controllers
{
    /// <summary>
    /// Контроллер відправлень користувача
    /// </summary>
    [Authorize]
    public class InvoicesController : Controller
    {
        private IDeliveryMessage deliveryMessage;

        private readonly IInvoicesService invoicesService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="deliveryMessage">Екзепляр повідолення користувача</param>
        /// <param name="invoicesService">Об'єкт сервісу відправлень</param>
        public InvoicesController(IDeliveryMessage deliveryMessage, IInvoicesService invoicesService)
        {
            this.deliveryMessage = deliveryMessage;
            this.invoicesService = invoicesService;
        }

        /// <summary>
        /// Повертає сторінку зі списком поштових відправлень користувача
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                string userId = User.Identity.GetUserId(); //"";// TODO - uncomment after testing process
                var invoicesDtos = invoicesService.GetInvoicesByUserId(userId);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<InvoiceDto, InvoiceViewModel>()
                    .ForMember("Notes", opt => opt.MapFrom(dto => dto.PostOperatorName + Environment.NewLine +
                              dto.Sender + Environment.NewLine +
                              dto.SenderAddress + Environment.NewLine +
                              dto.Recipient + Environment.NewLine +
                              dto.RecipientAddress + Environment.NewLine))).CreateMapper();
                List<InvoiceViewModel> invoicesViewModels = mapper
                    .Map<List<InvoiceViewModel>>(invoicesDtos);
                // TODO - код додавання властивостей PostOperatorName, PathToLogoImage або відповідну транзакцію в репо
                return View(invoicesViewModels);
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Відправлення";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Детальна інформація про відправлення
        /// </summary>
        /// <param name="id">Ідентифікатор відправлення</param>
        /// <returns>Сторінка інформації про відправлення</returns>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Не обрано відправлення для відображення.");
                var invoiceDto = invoicesService.GetById((int)id);
                if (invoiceDto == null) throw new Exception("Відправлення не знайдено.");

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<InvoiceDto, InvoiceViewModel>()
                    .ForMember("Notes", opt => opt.MapFrom(dto => dto.PostOperatorName + Environment.NewLine +
                                  dto.Sender + Environment.NewLine +
                                  dto.SenderAddress + Environment.NewLine +
                                  dto.Recipient + Environment.NewLine +
                                  dto.RecipientAddress + Environment.NewLine))).CreateMapper();
                //InvoiceViewModel invoice = mapper.Map<InvoiceViewModel>(invoiceDto);
                // TODO - код додавання властивостей PostOperatorName, PathToLogoImage або відповідну транзакцію в репо

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
        /// Get Повертає сторінку для створення нового відправлення
        /// </summary>
        /// <returns>Сторінка створення відправлення</returns>
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
        /// Отримує номер зі сторінки створення відправлення, перевіряє в списку існуючих, 
        /// шукає нові в інформаційних системах поштових операторів і зберігає в БД
        /// </summary>
        /// <param name="model">Модель пошуку поштового відправлення</param>
        /// <returns>Перехід до списку відстежуваних відправлень</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SearchInvoiceViewModel model)
        {
            try
            {
                invoicesService.Add("", model.Number, new Dictionary<string, string>
                    { { "ApiKeyNovaPoshta", WebConfigurationManager.AppSettings["ApiKeyNovaPoshta"] } });
                //invoicesService.Add(User.Identity.GetUserId(), model.Number, new Dictionary<string, string>
                //    { { "ApiKeyNovaPoshta", WebConfigurationManager.AppSettings["ApiKeyNovaPoshta"] } }); //TODO - uncomment in production (previous line is only for testing)
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
        /// Повертає сторінку для підтвердження видалення поштового відправлення
        /// </summary>
        /// <param name="id">Ідентифікатор відправлення</param>
        /// <returns>Сторінка підтвердження видалення відправлення</returns>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Не обрано відправлення для видалення.");
                InvoiceDto invoiceDto = invoicesService.GetById((int)id);
                if (invoiceDto == null) throw new Exception("Відправлення не знайдено.");

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<InvoiceDto, InvoiceViewModel>()
                    .ForMember("Notes", opt => opt.MapFrom(dto => dto.PostOperatorName + Environment.NewLine +
                                  dto.Sender + Environment.NewLine +
                                  dto.SenderAddress + Environment.NewLine +
                                  dto.Recipient + Environment.NewLine +
                                  dto.RecipientAddress + Environment.NewLine))).CreateMapper();

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
        /// Видаляє поштове відправлення користувача з БД
        /// </summary>
        /// <param name="id">Ідентифікатор відправлення</param>
        /// <param name="invoice">ViewModel відправлення</param>
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
        /// Оновлює статус відправлення
        /// </summary>
        /// <param name="id">Ідентифікатор відправлення</param>
        /// <returns>Перехід на сторінку інформації про відправлення</returns>
        [HttpPost]
        public ActionResult UpdateStatus(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Не обрано відправлення для оновлення статусу.");
                InvoiceDto invoiceDto = invoicesService.GetById((int)id);
                if (invoiceDto == null) throw new Exception("Відправлення не знайдено.");

                invoicesService.UpdateStatusAsync((int)id, new Dictionary<string, string>
                    { { "ApiKeyNovaPoshta", WebConfigurationManager.AppSettings["ApiKeyNovaPoshta"] } });

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
