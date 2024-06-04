using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Data.DTOs.CategoryDTOs;
using WebApi.Data.Responses;
using WebApi.Services;

namespace NUnitApp.TestControllers.TestCategoryController
{
    [TestFixture]
    public class Create
    {
        [Test]
        public async Task Create_Returns200_WhenCategoryCreatedSuccessfully()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            var createDto = new CreateCategoryDto { Name = "TestCategory", Description = "TestDescription" };
            var expectedResponse = new Response<string>("Successfully created category");

            mockCategoryService.Setup(service => service.CreateCategoryAsync(createDto))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.CreateCategoryAsync(createDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task Create_Returns400_WhenCategoryAlreadyExists()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            var createDto = new CreateCategoryDto { Name = "ExistingCategory", Description = "TestDescription" };
            var expectedResponse = new Response<string>(HttpStatusCode.BadRequest, "Category already exists");

            mockCategoryService.Setup(service => service.CreateCategoryAsync(createDto))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.CreateCategoryAsync(createDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(expectedResponse.Errors, ((Response<string>)result.Value).Errors);
        }

        [Test]
        public async Task Create_Returns500_WhenExceptionThrown()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            var createDto = new CreateCategoryDto { Name = "TestCategory", Description = "TestDescription" };

            mockCategoryService.Setup(service => service.CreateCategoryAsync(createDto))
                .ReturnsAsync(new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error"));

            // Act
            var result = await controller.CreateCategoryAsync(createDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
