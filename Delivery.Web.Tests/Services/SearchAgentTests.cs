using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Delivery.BLL.Services;
using Delivery.BLL.DTO;

namespace Delivery.Web.Tests.Services
{
    [TestClass]
    public class SearchAgentTests
    {
        private string errorMessage;
        private readonly ApiSearcherAgent apiAgent = new ApiSearcherAgent();
        private readonly HtmlSearcherAgent htmlAgent = new HtmlSearcherAgent();

        [TestInitialize]
        public void MyTestInitialize()
        {
            errorMessage = "";
        }

        [TestMethod]
        public void GetName_ShouldReturn_Name()
        {
            // Arrange
            string nameOfApiAgent = null, nameOfHtmlAgent = null;

            try
            {
                // Act 
                nameOfApiAgent = apiAgent.GetName();
                nameOfHtmlAgent = htmlAgent.GetName();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(nameOfApiAgent, errorMessage);
            Assert.IsNotNull(nameOfHtmlAgent, errorMessage);
        }

        [TestMethod]
        public void GetStatus_ShouldReturn_Status()
        {
            // Arrange
            string statusFromApiAgent = null, statusFromHtmlAgent = null;

            try
            {
                // Act 
                statusFromApiAgent = apiAgent.GetStatus("123456");// Put correct example of number
                statusFromHtmlAgent = htmlAgent.GetStatus("1234567890");// Put correct example of number
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(statusFromApiAgent, errorMessage);
            Assert.IsNotNull(statusFromHtmlAgent, errorMessage);
        }

        [TestMethod]
        public void SearchByNumber_ShouldReturn_InvoiceDto()
        {
            // Arrange
            InvoiceDto invoiceDtoFromApiAgent = null, invoiceDtoFromHtmlAgent = null;

            try
            {
                // Act 
                invoiceDtoFromApiAgent = apiAgent.SearchByNumber("123456");// Put correct example of number
                invoiceDtoFromHtmlAgent = htmlAgent.SearchByNumber("1234567890");// Put correct example of number
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(invoiceDtoFromApiAgent, errorMessage);
            Assert.IsNotNull(invoiceDtoFromHtmlAgent, errorMessage);
        }

    }
}
