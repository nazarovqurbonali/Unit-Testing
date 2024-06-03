using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Data.DTOs.CategoryDTOs;
using WebApi.Data.Responses;
using WebApi.Services;

namespace xUnitApp.TestControllers.CategoryController
{
    public class GetAll
    {
        [Fact]
        public async Task GetCategories_Returns200_WhenCategoriesExist()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            var expectedCategories = new List<GetCategoryDto>
            {
                new GetCategoryDto { Id = 1, Name = "Category1", Description = "Description1", CreatedAt = DateTimeOffset.UtcNow },
                new GetCategoryDto { Id = 2, Name = "Category2", Description = "Description2", CreatedAt = DateTimeOffset.UtcNow }
            };
            var expectedResponse = new Response<List<GetCategoryDto>>(expectedCategories);

            mockCategoryService.Setup(service => service.GetCategoriesAsync())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetCategoriesAsync() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedResponse.Data, ((Response<List<GetCategoryDto>>)result.Value).Data);
        }
        [Fact]
        public async Task GetCategories_Returns400_WhenErrorOccurs()
        {
            // Arrange
            var mockCategoryService = new Mock<ICategoryService>();
            var controller = new WebApi.Controllers.CategoryController(mockCategoryService.Object);
            var expectedResponse = new Response<List<GetCategoryDto>>(HttpStatusCode.BadRequest, "Error occurred");

            mockCategoryService.Setup(service => service.GetCategoriesAsync())
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.GetCategoriesAsync() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse.Errors, ((Response<List<GetCategoryDto>>)result.Value).Errors);
        }
    }
}