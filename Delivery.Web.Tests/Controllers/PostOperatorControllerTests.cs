using System;
using System.Collections.Generic;
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
    public class PostOperatorControllerTests
    {
        readonly Mock<IPostOperatorService> mockPostOperatorService = new Mock<IPostOperatorService>();

        private string errorMessage;

        private ViewResult result;

        [TestInitialize]
        public void TestInit()
        {
            errorMessage = "";
            result = null;
        }
        [TestMethod]
        public void Index_ShouldReturn_ViewAndListOfPostOperators()
        {
            // Arrange
            var postOperatorsDtos = new List<PostOperatorDto>
            {
                new PostOperatorDto
                {
                    Id= 1,
                    Name = "New Post",
                    LinkToSearchPage = "link1",
                    PathToLogoImage="path1",
                    IsActive = true,
                    Notes = "notes 1"
                },
                new PostOperatorDto
                {
                    Id= 2,
                    Name = "UkrPost",
                    LinkToSearchPage = "link2",
                    PathToLogoImage="path2",
                    IsActive = true,
                    Notes = "notes 3"
                }
            };
            mockPostOperatorService.Setup(po => po.GetAll()).Returns(postOperatorsDtos);
            PostOperatorController controller = new PostOperatorController(new DeliveryMessage(), mockPostOperatorService.Object);

            List<PostOperatorViewModel> postOperatorViewModels = null;
            try
            {
                // Act 
                result = controller.Index() as ViewResult;
                postOperatorViewModels = (List<PostOperatorViewModel>)result.ViewData.Model;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(result, errorMessage);
            Assert.IsTrue(postOperatorViewModels.Count == 2, errorMessage);
        }

        [TestMethod]
        public void Create_ShouldReturn_Success()
        {
            // Arrange
            mockPostOperatorService.Setup(i => i.Add(new PostOperatorDto
            {
                Name = "Federal Express",
                LinkToSearchPage = "link3",
                PathToLogoImage = "path3",
                IsActive = true,
                Notes = "notes 3"
            }));
            PostOperatorController controller = new PostOperatorController(new DeliveryMessage(), mockPostOperatorService.Object);

            try
            {
                // Act 
                result = controller.Create(new PostOperatorViewModel()) as ViewResult;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(result, errorMessage);
        }

        [TestMethod]
        public void Edit_Get_ShouldReturn_ViewAndListOfInvoices()
        {
            // Arrange
            var postOperatorDto = new PostOperatorDto
            {
                Id = 1,
                Name = "New Post",
                LinkToSearchPage = "link1",
                PathToLogoImage = "path1",
                IsActive = true,
                Notes = "notes 1"
            };
            mockPostOperatorService.Setup(po => po.GetById(postOperatorDto.Id)).Returns(postOperatorDto);
            PostOperatorController controller = new PostOperatorController(new DeliveryMessage(), mockPostOperatorService.Object);

            PostOperatorViewModel postOperator = null;
            try
            {
                // Act 
                result = controller.Edit(1) as ViewResult;
                postOperator = (PostOperatorViewModel)result.Model;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(result, errorMessage);
            Assert.IsNotNull(postOperator, errorMessage);
        }

        [TestMethod]
        public void Edit_Post_ShouldReturn_RedirectToAction()
        {
            // Arrange
            var postOperator = new PostOperatorViewModel { Id = 1 };
            mockPostOperatorService.Setup(po => po.UpdatePostOperator(new PostOperatorDto()));
            mockPostOperatorService.Setup(po => po.GetById(postOperator.Id)).Returns(new PostOperatorDto());
            PostOperatorController controller = new PostOperatorController(new DeliveryMessage(), mockPostOperatorService.Object);

            RedirectToRouteResult redirectResult = null;
            try
            {
                // Act 
                redirectResult = controller.Edit(postOperator) as RedirectToRouteResult;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(redirectResult, errorMessage);
        }
    }
}
