using Delivery.BLL.Services;
using Delivery.DAL.Models;
using Delivery.DAL.Repositories;
using Delivery.Web.Models;
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
        /// Реєструє усі залежності в системі
        /// </summary>
        public static void RegisterComponents()
        {
            string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Володимир\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\deliveryDB.mdf;Initial Catalog=deliveryDB;Integrated Security=True";
            string ef_connectionString = "DefaultConnection";

            var container = new UnityContainer()
                .RegisterType<IDeliveryMessage, DeliveryMessage>(new ContainerControlledLifetimeManager())

                .RegisterType<IAdminService, AdminService>(new InjectionConstructor(ef_connectionString))

                .RegisterType<IPostOperator, PostOperator>(new ContainerControlledLifetimeManager())
                .RegisterType<IPostOperatorsRepository, PostOperatorsRepository>(new InjectionConstructor(connectionString))
                .RegisterType<IPostOperatorService, PostOperatorService>(new InjectionConstructor(connectionString, new PostOperatorsRepository(connectionString)))

                .RegisterType<IInvoice, Invoice>(new ContainerControlledLifetimeManager())
                .RegisterType<IInvoicesRepository, InvoicesRepository>(new InjectionConstructor(connectionString))
                .RegisterType<IInvoicesService, InvoicesService>(new InjectionConstructor(connectionString, new InvoicesRepository(connectionString)));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}