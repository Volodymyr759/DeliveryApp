using AutoMapper;
using Delivery.BLL.DTO;
using Delivery.BLL.Services;
using Delivery.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Delivery.Web.Controllers
{
    /// <summary>
    /// PostOperators controller
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class PostOperatorController : Controller
    {
        private IDeliveryMessage deliveryMessage;

        private readonly IPostOperatorService postOperatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="deliveryMessage">Instance of the users message</param>
        /// <param name="postOperatorService">Object of the PostOperators service</param>
        /// <param name="mapper">Object map</param>
        public PostOperatorController(IDeliveryMessage deliveryMessage, 
            IPostOperatorService postOperatorService,
            IMapper mapper)
        {
            this.deliveryMessage = deliveryMessage;
            this.postOperatorService = postOperatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns page with the list of PostOperators
        /// </summary>
        /// <returns>page with the list of PostOperators</returns>
        public ActionResult Index()
        {
            try
            {
                return View("Index", mapper.Map<List<PostOperatorViewModel>>(postOperatorService.GetAll()));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Поштові оператори";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Returns page for creating the new PostOperator
        /// </summary>
        /// <returns>Page for creating the new PostOperator</returns>
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View("Create");
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Поштові оператори";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Creates the new PostOperator
        /// </summary>
        /// <param name="postOperator">ViewModel of the PostOperator</param>
        /// <returns>View for creating of the PostOperator</returns>
        [HttpPost]
        public ActionResult Create(PostOperatorViewModel postOperator)
        {
            try
            {
                postOperatorService.Add(mapper.Map<PostOperatorDto>(postOperator));

                return View("Create");
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Поштові оператори";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Returns page for edit PostOperator. Admin can edit only status.
        /// </summary>
        /// <param name="id">PostOperator's Id</param>
        /// <returns>View Edit</returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Не обрано поштового оператора для оновлення активності.");
                PostOperatorDto postOperatorDto = postOperatorService.GetById((int)id);
                if (postOperatorDto == null) throw new Exception("Поштового оператора не знайдено.");

                return View("Edit", mapper.Map<PostOperatorViewModel>(postOperatorDto));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Поштові оператори";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Saves edited data to database
        /// </summary>
        /// <param name="postOperator">ViewModel of the PostOperator</param>
        /// <returns>redirect to Index-page</returns>
        [HttpPost]
        public ActionResult Edit(PostOperatorViewModel postOperator)
        {
            try
            {
                if (postOperator == null) throw new Exception("Поштового оператора не знайдено.");
                postOperatorService.UpdatePostOperator(mapper.Map<PostOperatorDto>(postOperator));

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Поштові оператори";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }
    }
}
