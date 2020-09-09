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
    /// Admin controller
    /// </summary>
    public class AdminController : Controller
    {
        #region Private Properties

        private IDeliveryMessage deliveryMessage;

        private readonly IAdminService adminService;

        private readonly IInvoicesService invoicesService;

        private readonly IMapper mapper;

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
        /// <param name="deliveryMessage">Instance of the users message</param>
        /// <param name="adminService">Object of the aadmin service</param>
        /// <param name="invoicesService">Object of the Invoices service</param>
        /// <param name="mapper">Object  map</param>
        public AdminController(IDeliveryMessage deliveryMessage, 
            IAdminService adminService,
            IInvoicesService invoicesService,
            IMapper mapper)
        {
            this.deliveryMessage = deliveryMessage;
            this.adminService = adminService;
            this.invoicesService = invoicesService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Main page of Admin
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
        /// Returns list of all shipments for all users
        /// </summary>
        /// <returns>Список усіх відправлень</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Invoices()
        {
            try
            {
                // TODO - код додавання властивостей PostOperatorName, PathToLogoImage або відповідну транзакцію в репо
                return View(mapper.Map<List<InvoiceViewModel>>(invoicesService.GetAll()));
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
        /// Returns the list of registered users
        /// </summary>
        /// <returns>List of users</returns>
        public ActionResult Users()
        {
            try
            {
                return View(mapper.Map<List<AppUserViewModel>>(adminService.GetUsers()));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Users details
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Page of user info</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UserDetails(string userId)
        {
            try
            {
                return View("UserDetails", mapper.Map<AppUserViewModel>(adminService.GetUserById(userId)));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Returns confirmation for deleting the user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(string userId)
        {
            try
            {
                return View("DeleteUser", mapper.Map<AppUserViewModel>(adminService.GetUserById(userId)));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Адміністрування";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Deleting the user from db
        /// </summary>
        /// <param name="appUser">View model of the user</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(AppUserViewModel appUser)
        {
            try
            {
                invoicesService.RemoveByUser(appUser.Id);

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
        /// Returns Login page
        /// </summary>
        /// <param name="returnUrl">The page from which the user applied for logging in</param>
        /// <returns></returns>
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
        /// PGets data from the user's login page
        /// </summary>
        /// <param name="model">View model of the login</param>
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
        /// Logout from the system
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
        /// Returns the page for user registration
        /// </summary>
        /// <returns></returns>
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
        /// Gets data from the user registration page, adds a default role, creates default user settings
        /// </summary>
        /// <param name="model">View model of the registration</param>
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