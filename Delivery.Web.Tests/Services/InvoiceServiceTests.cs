using System;
using System.Collections.Generic;
using Delivery.BLL.DTO;
using Delivery.BLL.Services;
using Delivery.DAL.Models;
using Delivery.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Delivery.Web.Tests.Services
{
    [TestClass]
    public class InvoiceServiceTests
    {
        private string errorMessage;

        private bool operationSucceded;

        private readonly string connString = "";

        private readonly Mock<IInvoicesRepository> mockInvoicesRepo = new Mock<IInvoicesRepository>();

        [TestInitialize]
        public void MyTestInitialize()
        {
            errorMessage = "";
            operationSucceded = false;
        }

        [TestMethod]
        public void GetAll_ShouldReturn_ListOfInvoiceDtos()
        {
            // Arrange
            mockInvoicesRepo.Setup(i => i.GetAll()).Returns(new List<Invoice>());
            InvoicesService invoicesService = new InvoicesService(connString, mockInvoicesRepo.Object);

            List<InvoiceDto> invoiceDtos = null;
            try
            {
                // Act 
                invoiceDtos = (List<InvoiceDto>)invoicesService.GetAll();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(invoiceDtos, errorMessage);
        }

        [TestMethod]
        public void GetById_ShouldReturn_InvoiceDto()
        {
            // Arrange
            int invoiceId = 1;
            mockInvoicesRepo.Setup(i => i.GetById(invoiceId)).Returns(new Invoice());
            InvoicesService invoicesService = new InvoicesService(connString, mockInvoicesRepo.Object);

            InvoiceDto invoiceDto = null;
            try
            {
                // Act 
                invoiceDto = invoicesService.GetById(invoiceId);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(invoiceDto, errorMessage);
        }

        [TestMethod]
        public void GetInvoicesByUserId_ShouldReturn_ListOfInvoiceDtos()
        {
            // Arrange
            string userId = "";
            mockInvoicesRepo.Setup(i => i.GetByUserId(userId)).Returns(new List<Invoice>());
            InvoicesService invoicesService = new InvoicesService(connString, mockInvoicesRepo.Object);

            List<InvoiceDto> invoiceDtos = null;
            try
            {
                // Act 
                invoiceDtos = (List<InvoiceDto>)invoicesService.GetInvoicesByUserId(userId);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(invoiceDtos, errorMessage);
        }

        [TestMethod]
        public void Remove_ShouldReturn_Success()
        {
            // Arrange
            int invoiceId = 1;
            mockInvoicesRepo.Setup(i => i.Delete(invoiceId));
            InvoicesService invoicesService = new InvoicesService(connString, mockInvoicesRepo.Object);

            try
            {
                // Act 
                invoicesService.Remove(invoiceId);
                operationSucceded = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsTrue(operationSucceded, errorMessage);
        }

        [TestMethod]
        public void RemoveByUser_ShouldReturn_Success()
        {
            // Arrange
            string userId = "";
            mockInvoicesRepo.Setup(i => i.DeleteByUserId(userId));
            InvoicesService invoicesService = new InvoicesService(connString, mockInvoicesRepo.Object);

            try
            {
                // Act 
                invoicesService.RemoveByUser(userId);
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
