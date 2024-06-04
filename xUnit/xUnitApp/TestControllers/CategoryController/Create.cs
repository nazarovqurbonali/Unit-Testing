using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Data.DTOs.CategoryDTOs;
using WebApi.Data.Responses;
using WebApi.Services;

namespace xUnitApp.TestControllers.CategoryController
{
    public class CategoryControllerTests
    {
        [Fact]
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
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
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
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(expectedResponse.Errors, ((Response<string>)result.Value).Errors);
        }
    }
}