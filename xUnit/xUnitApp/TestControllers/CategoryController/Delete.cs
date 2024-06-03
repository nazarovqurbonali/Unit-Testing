using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Data.Responses;
using WebApi.Services;

namespace xUnitApp.TestControllers.CategoryController
{
    public class Delete
    {
        [Fact]
        public async Task Delete_Returns200_WhenCategoryDeletedSuccessfully()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            int categoryId = 1;
            var expectedResponse = new Response<bool>(true);

            mockCategoryService.Setup(service => service.DeleteCategoryAsync(categoryId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.DeleteCategoryAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedResponse.Data, ((Response<bool>)result.Value).Data);
        }

        [Fact]
        public async Task Delete_Returns400_WhenCategoryNotFound()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            int categoryId = 1;
            var expectedResponse = new Response<bool>(HttpStatusCode.BadRequest, "Category not found");

            mockCategoryService.Setup(service => service.DeleteCategoryAsync(categoryId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.DeleteCategoryAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse.Errors, ((Response<bool>)result.Value).Errors);
        }
    }
}
