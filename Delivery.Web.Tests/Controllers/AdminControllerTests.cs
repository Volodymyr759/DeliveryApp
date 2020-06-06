using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Delivery.BLL.Services;
using System.Web.Mvc;
using Delivery.BLL.DTO;
using Delivery.Web.Controllers;
using Delivery.Web.Models;
using System.Threading.Tasks;

namespace Delivery.Web.Tests.Controllers
{
    /// <summary>
    /// Summary description for AdminControllerTests
    /// </summary>
    [TestClass]
    public class AdminControllerTests
    {
        readonly Mock<IAdminService> mockAdminService = new Mock<IAdminService>();

        readonly Mock<IInvoicesService> mockInvoicesService = new Mock<IInvoicesService>();

        private string errorMessage;

        private ViewResult result;

        [TestInitialize]
        public void TestInit()
        {
            errorMessage = "";
            result = null;
        }

        [TestMethod]
        public void Users_ShouldReturn_View()
        {
            // Arrange
            IEnumerable<AppUserDto> usersDtos = new List<AppUserDto>
            {
                new AppUserDto{}, new AppUserDto{}
            };
            mockAdminService.Setup(u => u.GetUsers()).Returns(usersDtos);
            AdminController controller = new AdminController(new DeliveryMessage(), mockAdminService.Object,
                mockInvoicesService.Object);

            try
            {
                // Act 
                result = controller.Users() as ViewResult;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(result.ViewName == "Users", errorMessage);
        }

        [TestMethod]
        public void UserDetails_ShouldReturn_ViewAndDetailInfo()
        {
            // Arrange
            string userId = "asd1";
            var userDto = new AppUserDto { Id = userId };
            mockAdminService.Setup(u => u.GetUserById(userId)).Returns(userDto);
            AdminController controller = new AdminController(new DeliveryMessage(), mockAdminService.Object,
                mockInvoicesService.Object);

            AppUserViewModel userViewModel = null;
            try
            {
                // Act 
                result = controller.UserDetails(userId) as ViewResult;
                userViewModel = (AppUserViewModel)result.Model;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsTrue(result.ViewName == "UserDetails", errorMessage);
            Assert.IsNotNull(userViewModel, errorMessage);
        }

        [TestMethod]
        public void DeleteUser_ShouldReturn_Success()
        {
            // Arrange
            string userId = "";
            AppUserDto userDto = new AppUserDto();

            mockAdminService.Setup(u => u.GetUserById(userId)).Returns(userDto);
            AdminController controller = new AdminController(new DeliveryMessage(), mockAdminService.Object,
                mockInvoicesService.Object);

            try
            {
                // Act 
                result = controller.DeleteUser(userId) as ViewResult;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsTrue(result.ViewName == "DeleteUser", errorMessage);
        }

        [TestMethod]
        public void Invoices_ShouldReturn_ViewAndListOfInvoices()
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
            mockInvoicesService.Setup(i => i.GetAll()).Returns(invoicesDtos);
            AdminController controller = new AdminController(new DeliveryMessage(), mockAdminService.Object,
                mockInvoicesService.Object);

            IEnumerable<InvoiceViewModel> invoiceViewModels = null;
            try
            {
                // Act 
                result = controller.Invoices() as ViewResult;
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
    }
}
