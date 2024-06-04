using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Data.DTOs.CategoryDTOs;
using WebApi.Data.Responses;
using WebApi.Services;

namespace NUnitApp.TestControllers.TestCategoryController
{
    [TestFixture]
    public class GetById
    {
        [Test]
        public async Task GetById_Returns200_WhenCategoryRetrievedSuccessfully()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            const int categoryId = 1;
            var expectedCategory = new GetCategoryDto
                { Id = categoryId, Name = "TestCategory", Description = "TestDescription" };
            var expectedResponse = new Response<GetCategoryDto>(expectedCategory);

            mockCategoryService.Setup(service => service.GetCategoryByIdAsync(categoryId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetCategoryByIdAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task GetById_Returns400_WhenCategoryNotFound()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            const int categoryId = 1;
            var expectedResponse = new Response<GetCategoryDto>(HttpStatusCode.NotFound, "Category not found");

            mockCategoryService.Setup(service => service.GetCategoryByIdAsync(categoryId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetCategoryByIdAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual(expectedResponse.Errors, ((Response<GetCategoryDto>)result.Value).Errors);
        }

        [Test]
        public async Task GetById_Returns500_WhenExceptionThrown()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            const int categoryId = 1;

            mockCategoryService.Setup(service => service.GetCategoryByIdAsync(categoryId))
                .ReturnsAsync(new Response<GetCategoryDto>(HttpStatusCode.InternalServerError,
                    "Internal Server Error"));

            // Act
            var result = await controller.GetCategoryByIdAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}