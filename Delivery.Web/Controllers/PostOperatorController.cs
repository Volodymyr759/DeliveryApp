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
    /// Контроллер управління даними поштових операторів
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class PostOperatorController : Controller
    {
        private IDeliveryMessage deliveryMessage;

        private readonly IPostOperatorService postOperatorService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="deliveryMessage">Екзепляр повідолення користувача</param>
        /// <param name="postOperatorService">Об'єкт сервісу поштових операторів</param>
        public PostOperatorController(IDeliveryMessage deliveryMessage, IPostOperatorService postOperatorService)
        {
            this.deliveryMessage = deliveryMessage;
            this.postOperatorService = postOperatorService;
        }

        /// <summary>
        /// Повертає сторінку зі списком поштових операторів, реалізованих в системі Delivery
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                var postOperatorsDtos = postOperatorService.GetAll();
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperatorDto, PostOperatorViewModel>()).CreateMapper();

                return View("Index", mapper.Map<List<PostOperatorViewModel>>(postOperatorsDtos));
            }
            catch (Exception ex)
            {
                deliveryMessage.Title = "Поштові оператори";
                deliveryMessage.Body = ex.Message;
                return View("DeliveryMessage", deliveryMessage);
            }
        }

        /// <summary>
        /// Get Повертає сторінку для створення нового поштового оператора
        /// </summary>
        /// <returns>Сторінка створення поштового оператора</returns>
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
        /// Створює нового поштового оператора (при програмній реалізації нового поштового оператора)
        /// </summary>
        /// <param name="postOperator">ViewModel поштового оператора</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(PostOperatorViewModel postOperator)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperatorViewModel, PostOperatorDto>()).CreateMapper();
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
        /// Повертає сторінку редагування даних поштового оператора. Адміністратор може редагувати 
        /// тільки статус активності оператора (відключає при збоях в доступі до інформаційної системи оператора)
        /// </summary>
        /// <param name="id">Ідентифікатор поштового оператора</param>
        /// <returns></returns>
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null) throw new Exception("Не обрано поштового оператора для оновлення активності.");
                PostOperatorDto postOperatorDto = postOperatorService.GetById((int)id);
                if (postOperatorDto == null) throw new Exception("Поштового оператора не знайдено.");
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperatorDto, PostOperatorViewModel>()).CreateMapper();

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
        /// Post Отримує дані зі сторінки редагування поштового оператора і зберігає в БД
        /// </summary>
        /// <param name="postOperator">ViewModel поштового оператора</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(PostOperatorViewModel postOperator)
        {
            try
            {
                if (postOperator == null) throw new Exception("Поштового оператора не знайдено.");
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperatorViewModel, PostOperatorDto>()).CreateMapper();
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
