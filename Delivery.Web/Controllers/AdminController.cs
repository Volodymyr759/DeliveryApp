using Delivery.BLL.Services;
using Microsoft.Owin.Security;
using Delivery.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Delivery.BLL.DTO;
using System.Security.Claims;
using System;
using AutoMapper;

namespace Delivery.Web.Controllers
{
    /// <summary>
    /// Контроллер адміністратора
    /// </summary>
    public class AdminController : Controller
    {
        #region Private Properties

        private IDeliveryMessage deliveryMessage;

        private readonly IAdminService adminService;

        private readonly IInvoicesService invoicesService;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="deliveryMessage">Екзепляр повідолення користувача</param>
        /// <param name="adminService">Об'єкт сервісу адміністратора</param>
        /// <param name="invoicesService">Об'єкт сервісу відправлень</param>
        public AdminController(IDeliveryMessage deliveryMessage, IAdminService adminService,
            IInvoicesService invoicesService)
        {
            this.deliveryMessage = deliveryMessage;
            this.adminService = adminService;
            this.invoicesService = invoicesService;
        }

        /// <summary>
        /// Головна сторінка адміністратора
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Повертає список усіх створених відправлень усіх користувачів
        /// </summary>
        /// <returns>Список усіх відправлень</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Invoices()
        {
            try
            {
                var invoicesDtos = invoicesService.GetAll();
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
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        #region ManageUsers

        /// <summary>
        /// Повертає список користувачів, зареєстрованих на Delivery
        /// </summary>
        /// <returns>список користувачів</returns>
        public ActionResult Users()
        {
            try
            {
                var appUsersDtos = adminService.GetUsers();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUserDto, AppUserViewModel>()).CreateMapper();
                List<AppUserViewModel> appUsersViewModels = mapper.Map<List<AppUserViewModel>>(appUsersDtos);
                return View(appUsersViewModels);
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Детальна інформація про користувача
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Сторінка інформації про користувача</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UserDetails(string userId)
        {
            try
            {
                var appUserDto = adminService.GetUserById(userId);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUserDto, AppUserViewModel>()).CreateMapper();

                return View("UserDetails", mapper.Map<AppUserViewModel>(appUserDto));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Повертає сторінку для підтвердження видалення користувача
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Сторінка підтвердження видалення користувача</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(string userId)
        {
            try
            {
                var appUserDto = adminService.GetUserById(userId);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUserDto, AppUserViewModel>()).CreateMapper();

                return View("DeleteUser", mapper.Map<AppUserViewModel>(appUserDto));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Видаляє користувача з БД
        /// </summary>
        /// <param name="appUser">ViewModel користувача</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(AppUserViewModel appUser)
        {
            try
            {
                invoicesService.RemoveByUser(appUser.Id);// пов'язані записи відправлень користувача

                adminService.RemoveUser(appUser.Id);

                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        #endregion

        #region Log in @ Registration

        /// <summary>
        /// Get Повертає сторінку логування
        /// </summary>
        /// <param name="returnUrl">Сторінка з якої користувач звернувся для логування</param>
        /// <returns>Сторінка логування користувача</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();

            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Post Отримує дані зі сторінки логування користувача 
        /// </summary>
        /// <param name="model">ViewModel логування користувача</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUserDto userDto = new AppUserDto { Email = model.Email, Password = model.Password };
                    ClaimsIdentity claim = await adminService.Authenticate(userDto);
                    if (claim == null)
                    {
                        ModelState.AddModelError("", "Невірний логін або пароль.");
                    }
                    else
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        return RedirectToAction("Index", "Main");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Вихід користувача з облікового запису
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            try
            {
                AuthenticationManager.SignOut();
                return RedirectToAction("Index", "Main");
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Повертає сторінку для реєстрації користувача
        /// </summary>
        /// <returns>Сторінка реєстрації користувача</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Post Отримує дані зі сторінки реєстрації користувача, додає роль по замовчуванню, 
        /// створює налаштування користувача по замовчуванню
        /// </summary>
        /// <param name="model">ViewModel реєстрації користувача</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUserDto userDto = new AppUserDto
                    {
                        Email = model.Email,
                        Password = model.Password,
                    };
                    await adminService.AddUser(userDto);
                    await Login(new LoginViewModel { Email = model.Email, Password = model.Password });
                }
                return View(model);

            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        #endregion
    }
}