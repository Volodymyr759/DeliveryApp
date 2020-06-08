using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Delivery.BLL.DTO;
using Delivery.BLL.Services;
using Delivery.Web.Controllers;
using Delivery.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Delivery.Web.Tests.Controllers
{
    [TestClass]
    public class MainControllerTests
    {
        private Mock<IPostOperatorService> mockPostOperatorService ;
        private Mock<IInvoicesService> mockInvoicesService;

        private string errorMessage;
        private ViewResult result;

        [TestInitialize]
        public void TestInit()
        {
            mockPostOperatorService = new Mock<IPostOperatorService>();
            mockInvoicesService = new Mock<IInvoicesService>();
            errorMessage = "";
            result = null;
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            mockPostOperatorService = null;
            mockInvoicesService = null;
        }

        [TestMethod]
        public void Index_ShouldReturn_ViewAndNotEmptyModel()
        {
            // Arrange
            var postOperators = new List<PostOperatorDto>
            {
                new PostOperatorDto{ Id=1, Name = "Нова Пошта", LinkToSearchPage= "https://novaposhta.ua/tracking", PathToLogoImage="", IsActive=true},
                new PostOperatorDto{ Id=1, Name = "Укрпошта", LinkToSearchPage= "https://track.ukrposhta.ua/tracking_UA.html", PathToLogoImage="", IsActive=true}
            };
            mockPostOperatorService.Setup(pos => pos.GetAll()).Returns(postOperators);
            MainController controller = new MainController(new DeliveryMessage(), mockPostOperatorService.Object, mockInvoicesService.Object);

            MainIndexViewModel model = null;
            try
            {
                // Act
                result = controller.Index() as ViewResult;
                model = (MainIndexViewModel)result.ViewData.Model;

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }
            // Assert
            Assert.IsNotNull(result, errorMessage);
            Assert.IsTrue(model.PostOperators.Count() > 0, errorMessage);
        }

        [TestMethod]
        public void Invoice_Post_CorrectNumber_ShouldReturn_ViewAndNotEmtyInvoice()
        {
            // Arrange
            InvoiceDto invoiceDto = new InvoiceDto { };
            mockInvoicesService.Setup(s => s.SearchByNumber("123456", new Dictionary<string, string>())).Returns(invoiceDto);
            MainController controller = new MainController(new DeliveryMessage(), mockPostOperatorService.Object, mockInvoicesService.Object);

            InvoiceViewModel invoiceViewModel = null;
            try
            {
                // Act
                result = controller.Index(new MainIndexViewModel { Number = "123456" }) as ViewResult;
                invoiceViewModel = (InvoiceViewModel)result.Model;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsTrue(result.ViewName == "Details", errorMessage);
            Assert.IsNotNull(invoiceViewModel, errorMessage);
        }

        [TestMethod]
        public void Invoice_Post_UnCorrectNumber_ShouldReturn_DeliveryMessage()
        {
            // Arrange
            InvoiceDto invoiceDto = null;
            mockInvoicesService.Setup(s => s.SearchByNumber("", new Dictionary<string, string>())).Returns(invoiceDto);
            MainController controller = new MainController(new DeliveryMessage(), mockPostOperatorService.Object, mockInvoicesService.Object);

            DeliveryMessage deliveryMessage = null;
            try
            {
                // Act
                result = controller.Index(new MainIndexViewModel { Number = "" }) as ViewResult;
                deliveryMessage = (DeliveryMessage)result.Model;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsTrue(result.ViewName == "DeliveryMessage", errorMessage);
            Assert.IsNotNull(deliveryMessage, errorMessage);
        }
    }
}
