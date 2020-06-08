using Delivery.BLL.Services;
using Delivery.DAL.Models;
using Delivery.DAL.Repositories;
using Delivery.Web.Models;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;

namespace Delivery.Web.App_Start
{
    /// <summary>
    /// Клас-конфігуратор Unity-контейнера для реєстрації залежностей
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Реєструє залежності в системі
        /// </summary>
        public static void RegisterComponents()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DeliveryConnection"].ConnectionString;
            Dictionary<string, string> apiKeys = new Dictionary<string, string>
                {
                    { "ApiKeyNovaPoshta", WebConfigurationManager.AppSettings["ApiKeyNovaPoshta"] }
                };

            var container = new UnityContainer()
                .RegisterType<IDeliveryMessage, DeliveryMessage>(new ContainerControlledLifetimeManager())

                .RegisterType<IAdminService, AdminService>(new InjectionConstructor(connectionString))

                .RegisterType<IPostOperator, PostOperator>(new ContainerControlledLifetimeManager())
                .RegisterType<IPostOperatorsRepository, PostOperatorsRepository>(new InjectionConstructor(connectionString))
                .RegisterType<IPostOperatorService, PostOperatorService>(new InjectionConstructor(connectionString, new PostOperatorsRepository(connectionString)))

                .RegisterType<IInvoice, Invoice>(new ContainerControlledLifetimeManager())
                .RegisterType<IInvoicesRepository, InvoicesRepository>(new InjectionConstructor(connectionString))
                .RegisterType<IInvoicesService, InvoicesService>(new InjectionConstructor(connectionString, new InvoicesRepository(connectionString), apiKeys));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}