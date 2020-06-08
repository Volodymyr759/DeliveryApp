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
    public class PostOperatorServiceTests
    {
        private string errorMessage;

        private bool operationSucceded;

        private readonly string connString = "";

        private Mock<IPostOperatorsRepository> mockPostOperatorsRepo;

        [TestInitialize]
        public void MyTestInitialize()
        {
            mockPostOperatorsRepo = new Mock<IPostOperatorsRepository>();
            errorMessage = "";
            operationSucceded = false;
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            mockPostOperatorsRepo = null;
        }

        [TestMethod]
        public void Add_ShouldReturn_Success()
        {
            //Arrange
            PostOperatorDto postOperatorDto = new PostOperatorDto
            {
                Name = "Нова Пошта",
                LinkToSearchPage = "link123",
                PathToLogoImage = "path123",
                IsActive = true,
                Notes = "notes 3"
            };
            PostOperator postOperator = new PostOperator
            {
                Name = postOperatorDto.Name,
                LinkToSearchPage = postOperatorDto.LinkToSearchPage,
                PathToLogoImage = postOperatorDto.PathToLogoImage,
                IsActive = postOperatorDto.IsActive,
                Notes = postOperatorDto.Notes
            };

            mockPostOperatorsRepo.Setup(po => po.Create(postOperator));
            PostOperatorService postOperatorService = new PostOperatorService(connString, mockPostOperatorsRepo.Object);
            try
            {
                // Act 
                postOperatorService.Add(postOperatorDto);
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
        public void GetById_ShouldReturn_PostOperatorDto()
        {
            // Arrange
            int id = 1;
            mockPostOperatorsRepo.Setup(po => po.GetById(id)).Returns(new PostOperator());
            PostOperatorService postOperatorService = new PostOperatorService(connString, mockPostOperatorsRepo.Object);

            PostOperatorDto postOperatorDto = null;
            try
            {
                // Act 
                postOperatorDto = postOperatorService.GetById(id);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(postOperatorDto, errorMessage);
        }

        [TestMethod]
        public void GetAll_ShouldReturn_ListOfPostOperatorDtos()
        {
            // Arrange
            mockPostOperatorsRepo.Setup(po => po.GetAll()).Returns(new List< PostOperator>());
            PostOperatorService postOperatorService = new PostOperatorService(connString, mockPostOperatorsRepo.Object);

            List<PostOperatorDto> postOperatorDtos = null;
            try
            {
                // Act 
                postOperatorDtos = (List<PostOperatorDto>)postOperatorService.GetAll();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " | " + ex.StackTrace;
            }

            // Assert
            Assert.IsNotNull(postOperatorDtos, errorMessage);
        }

        [TestMethod]
        public void Update_ShouldReturn_Success()
        {
            // Arrange
            PostOperator postOperator = new PostOperator
            {
                Name = "Нова Пошта",
                LinkToSearchPage = "link123",
                PathToLogoImage = "path3",
                IsActive = false,
                Notes = "notes 3"
            };
            PostOperatorDto postOperatorDto = new PostOperatorDto
            {
                Name = "Нова Пошта",
                LinkToSearchPage = "link123",
                PathToLogoImage = "path3",
                IsActive = true,
                Notes = "notes 3"
            };
            mockPostOperatorsRepo.Setup(po => po.Update(postOperator));
            PostOperatorService postOperatorService = new PostOperatorService(connString, mockPostOperatorsRepo.Object);

            try
            {
                // Act 
                postOperatorService.UpdatePostOperator(postOperatorDto);
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
