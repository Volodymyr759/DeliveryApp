using Microsoft.VisualStudio.TestTools.UnitTesting;
using Delivery.Web.Controllers;
using System;
using System.Collections.Generic;
using Moq;
using Delivery.BLL.Services;
using System.Web.Mvc;
using Delivery.BLL.DTO;
using Delivery.Web.Models;

namespace Delivery.Web.Tests
{
    [TestClass]
    public class InvoicesControllerTests
    {
        private Mock<IInvoicesService> mockInvoicesService;

        private string errorMessage;

        private ViewResult result;

        [TestInitialize]
        public void TestInit()
        {
            mockInvoicesService = new Mock<IInvoicesService>();
            errorMessage = "";
            result = null;
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            mockInvoicesService = null;
        }

        [TestMethod]
        public void Index_ShouldReturn_ViewAndListOfInvoices()
        {
            // Arrange
            var invoicesDtos = new List<InvoiceDto>
            {
                new InvoiceDto
                {
                    Id= 1,
                    PostOperatorName = "",
                    Number="1234567890123",
                    SendDateTime = DateTime.Parse("2020.06.01"),
                    Sender = "Sender 1",
                    SenderAddress = "Sender 1 address",
                    Recipient = "Recipient 1",
                    RecipientAddress = "Recipient 1 Address",
                    CurrentLocation = "Current location 1",
                    ActualStatus = "Actual status 1"
                },
                new InvoiceDto
                {
                    Id= 2,
                    PostOperatorName = "",
                    Number="1234567890124",
                    SendDateTime = DateTime.Parse("2020.06.02"),
                    Sender = "Sender 2",
                    SenderAddress = "Sender 2 address",
                    Recipient = "Recipient 1",
                    RecipientAddress = "Recipient 1 Address",
                    CurrentLocation = "Current location 1",
                    ActualStatus = "Actual status 2"
                }
            };
            mockInvoicesService.Setup(i => i.GetInvoicesByUserId("")).Returns(invoicesDtos);
            InvoicesController controller = new InvoicesController(new DeliveryMessage(), mockInvoicesService.Object);

            IEnumerable<InvoiceViewModel> invoiceViewModels = null;
            try
            {
                // Act 
                result = controller.Index() as ViewResult;
                invoiceViewModels = (List<InvoiceViewModel>)result.ViewData.Model;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(result, errorMessage);
            Assert.IsNotNull(invoiceViewModels, errorMessage);
        }

        [TestMethod]
        public void Details_ShouldReturn_ViewAndDetailInfo()
        {
            // Arrange
            int id = 1;
            var invoiceDto = new InvoiceDto
            {
                Id = id,
                PostOperatorName = "",
                Number = "1234567890123",
                SendDateTime = DateTime.Parse("2020.06.01"),
                Sender = "Sender 1",
                SenderAddress = "Sender 1 address",
                Recipient = "Recipient 1",
                RecipientAddress = "Recipient 1 Address",
                CurrentLocation = "Current location 1",
                ActualStatus = "Actual status 1",
            };
            mockInvoicesService.Setup(i => i.GetById(id)).Returns(invoiceDto);
            InvoicesController controller = new InvoicesController(new DeliveryMessage(), mockInvoicesService.Object);

            InvoiceViewModel invoiceViewModel = null;
            try
            {
                // Act 
                result = controller.Details(id) as ViewResult;
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
        public void Create_ShouldReturn_Success()
        {
            // Arrange
            mockInvoicesService.Setup(i => i.Add("userId", "number"));
            InvoicesController controller = new InvoicesController(new DeliveryMessage(), mockInvoicesService.Object);

            try
            {
                // Act 
                result = controller.Create(new SearchInvoiceViewModel()) as ViewResult;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(result, errorMessage);
        }

        [TestMethod]
        public void Delete_ShouldReturn_Success()
        {
            // Arrange
            int id = 1;
            mockInvoicesService.Setup(i => i.Remove(id));
            InvoicesController controller = new InvoicesController(new DeliveryMessage(), mockInvoicesService.Object);

            try
            {
                // Act 
                result = controller.Delete(id) as ViewResult;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(result, errorMessage);
        }

        [TestMethod]
        public void Update_ShouldReturn_Success()
        {
            // Arrange
            int id = 1;
            mockInvoicesService.Setup(i => i.UpdateStatusAsync(id));
            mockInvoicesService.Setup(i => i.GetById(id)).Returns(new InvoiceDto { Id = 1 });
            InvoicesController controller = new InvoicesController(new DeliveryMessage(), mockInvoicesService.Object);
            bool operationSucceded = false;

            try
            {
                // Act 
                controller.UpdateStatus(id);
                operationSucceded = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }
            // Assert
            Assert.IsTrue(operationSucceded, errorMessage);
        }
    }
}