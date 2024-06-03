using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Data.DTOs.CategoryDTOs;
using WebApi.Data.Responses;
using WebApi.Services;

namespace xUnitApp.TestControllers.CategoryController
{
    public class GetById
    {
        [Fact]
        public async Task GetCategoryById_Returns200_WhenCategoryFound()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            int categoryId = 1;
            var expectedCategory = new GetCategoryDto { Id = categoryId, Name = "Category1", Description = "Description1", CreatedAt = DateTimeOffset.UtcNow };
            var expectedResponse = new Response<GetCategoryDto>(expectedCategory);

            mockCategoryService.Setup(service => service.GetCategoryByIdAsync(categoryId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetCategoryByIdAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedResponse.Data, ((Response<GetCategoryDto>)result.Value).Data);
        }
        [Fact]
        public async Task GetCategoryById_Returns400_WhenCategoryNotFound()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            int categoryId = 1;
            var expectedResponse = new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Category not found");

            mockCategoryService.Setup(service => service.GetCategoryByIdAsync(categoryId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetCategoryByIdAsync(categoryId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse.Errors, ((Response<GetCategoryDto>)result.Value).Errors);
        }
    }
}