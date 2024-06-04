using WebApi.Data.DTOs.CategoryDTOs;
using WebApi.Services;
using xUnitApp.FakeDbContext;

namespace xUnitApp.Services
{
    public class TestCategoryService
    {
        [Fact]
        public async Task Add_CategoryService_Success()
        {
            // Arrange
            var category = new CreateCategoryDto()
            {
                Name = "testNew",
                Description = "testNew",
            };

            var context = new FakeDbContextFactory();
            var categoryService = new CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.CreateCategoryAsync(category);

            // Assert
            Assert.Equal(200, res.StatusCode);
        }

        [Fact]
        public async Task Add_CategoryService_Failure()
        {
            var context = new FakeDbContextFactory();

            // Arrange
            var categoryService = new CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.CreateCategoryAsync(null);

            // Assert
            Assert.Equal(400, res.StatusCode);
        }

        [Fact]
        public async Task Edit_CategoryService_Success()
        {
            const int id = 1;
            // Arrange
            var category = new UpdateCategoryDto()
            {
                Id = id,
                Description = "testNew-update",
                Name = "testNew-update",
            };
            var context = new FakeDbContextFactory();

            var categoryService = new CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.UpdateCategoryAsync(category);

            // Assert
            Assert.Equal(200, res.StatusCode);
        }

        [Fact]
        public async Task Edit_CategoryService_Failure()
        {
            // Arrange
            var category = new UpdateCategoryDto();
            var context = new FakeDbContextFactory();

            var categoryService = new CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.UpdateCategoryAsync(category);

            // Assert
            Assert.Equal(400, res.StatusCode);
        }

        [Fact]
        public async Task Delete_CategoryService_Success()
        {
            const int id = 1;
            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.DeleteCategoryAsync(id);

            // Assert
            Assert.True(res.Data);
        }

        [Fact]
        public async Task Delete_CategoryService_Failure()
        {
            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.DeleteCategoryAsync(0);

            // Assert
            Assert.False(res.Data);
        }

        [Fact]
        public async Task GetById_CategoryService_Success()
        {
            const int id = 1;

            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.GetCategoryByIdAsync(id);

            // Assert
            Assert.Equal(200, res.StatusCode);
        }

        [Fact]
        public async Task GetById_CategoryService_Failure()
        {
            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.GetCategoryByIdAsync(0);

            // Assert
            Assert.Equal(400, res.StatusCode);
        }

        [Fact]
        public async Task GetAll_CategoryService_Success()
        {
            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.GetCategoriesAsync();

            // Assert
            Assert.NotNull(res.Data);
            Assert.Equal(200,res.StatusCode);
        }
    }
}