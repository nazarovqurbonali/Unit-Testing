using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Data.DTOs.CategoryDTOs;
using WebApi.Data.Responses;
using WebApi.Services;

namespace xUnitApp.TestControllers.CategoryController
{
    public class Update
    {
        [Fact]
        public async Task UpdateCategory_Returns200_WhenCategoryUpdatedSuccessfully()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            var updateDto = new UpdateCategoryDto
                { Id = 1, Name = "UpdatedCategory", Description = "UpdatedDescription" };
            var expectedResponse = new Response<string>("Successfully updated category");

            mockCategoryService.Setup(service => service.UpdateCategoryAsync(updateDto))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.UpdateCategoryAsync(updateDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedResponse.Data, ((Response<string>)result.Value).Data);
        }

        [Fact]
        public async Task UpdateCategory_Returns400_WhenCategoryNotFound()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            var updateDto = new UpdateCategoryDto
                { Id = 999, Name = "NonExistingCategory", Description = "Description" };
            var expectedResponse = new Response<string>(HttpStatusCode.BadRequest, "Not found category with Id=999");

            mockCategoryService.Setup(service => service.UpdateCategoryAsync(updateDto))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.UpdateCategoryAsync(updateDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse.Errors, ((Response<string>)result.Value).Errors);
        }
    }
}