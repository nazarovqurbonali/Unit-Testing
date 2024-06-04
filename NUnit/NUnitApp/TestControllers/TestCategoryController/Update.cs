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
    public class Update
    {
        [Test]
        public async Task Update_Returns200_WhenCategoryUpdatedSuccessfully()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            var updateDto = new UpdateCategoryDto { Id = 1, Name = "UpdatedCategory", Description = "TestDescription" };
            var expectedResponse = new Response<string>("Successfully updated category");

            mockCategoryService.Setup(service => service.UpdateCategoryAsync(updateDto))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.UpdateCategoryAsync(updateDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task Update_Returns400_WhenCategoryNotFound()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            var updateDto = new UpdateCategoryDto { Id = 1, Name = "UpdatedCategory", Description = "TestDescription" };
            var expectedResponse = new Response<string>(HttpStatusCode.BadRequest, "Category not found");

            mockCategoryService.Setup(service => service.UpdateCategoryAsync(updateDto))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.UpdateCategoryAsync(updateDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(expectedResponse.Errors, ((Response<string>)result.Value).Errors);
        }

        [Test]
        public async Task Update_Returns500_WhenExceptionThrown()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new CategoryController(mockCategoryService.Object);
            var updateDto = new UpdateCategoryDto { Id = 1, Name = "UpdatedCategory", Description = "TestDescription" };

            mockCategoryService.Setup(service => service.UpdateCategoryAsync(updateDto))
                .ReturnsAsync(new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error"));

            // Act
            var result = await controller.UpdateCategoryAsync(updateDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
