using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Data.Responses;
using WebApi.Services;

namespace NUnitApp.TestControllers.TestCategoryController
{
    [TestFixture]
    public class Delete
    {
        [Test]
        public async Task Delete_Returns200_WhenCategoryDeletedSuccessfully()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            const int categoryId = 1;
            var expectedResponse = new Response<bool>(true);

            mockCategoryService.Setup(service => service.DeleteCategoryAsync(categoryId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.DeleteCategoryAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task Delete_Returns400_WhenCategoryNotFound()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            const int categoryId = 1;
            var expectedResponse = new Response<bool>(HttpStatusCode.NotFound, "Category not found");

            mockCategoryService.Setup(service => service.DeleteCategoryAsync(categoryId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.DeleteCategoryAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual(expectedResponse.Errors, ((Response<bool>)result.Value).Errors);
        }

        [Test]
        public async Task Delete_Returns500_WhenExceptionThrown()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            const int categoryId = 1;

            mockCategoryService.Setup(service => service.DeleteCategoryAsync(categoryId))
                .ReturnsAsync(new Response<bool>(HttpStatusCode.InternalServerError,"Internal Server Error"));

            // Act
            var result = await controller.DeleteCategoryAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
