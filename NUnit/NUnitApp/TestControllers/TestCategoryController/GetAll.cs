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
    public class GetAll
    {
        [Test]
        public async Task GetAll_Returns200_WhenCategoriesRetrievedSuccessfully()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            var expectedCategories = new List<GetCategoryDto>
            {
                new () { Id = 1, Name = "TestCategory1", Description = "TestDescription1" },
                new () { Id = 2, Name = "TestCategory2", Description = "TestDescription2" }
            };
            var expectedResponse = new Response<List<GetCategoryDto>>(expectedCategories);

            mockCategoryService.Setup(service => service.GetCategoriesAsync())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetCategoriesAsync() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task GetAll_Returns500_WhenExceptionThrown()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);

            mockCategoryService.Setup(service => service.GetCategoriesAsync())
                .ReturnsAsync(new Response<List<GetCategoryDto>>(HttpStatusCode.InternalServerError,"Internal Server Error"));

            // Act
            var result = await controller.GetCategoriesAsync() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
