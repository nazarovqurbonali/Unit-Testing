using NUnitApp.FakeDbContext;
using WebApi.Data.DTOs.CategoryDTOs;
using WebApi.Services;

namespace NUnitApp.TestServices
{
    [TestFixture]
    public class TestCategoryService
    {
      

        [Test]
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

            var categoryService = new WebApi.Services.CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.UpdateCategoryAsync(category);

            // Assert
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public async Task Edit_CategoryService_Failure()
        {
            // Arrange
            var category = new UpdateCategoryDto();
            var context = new FakeDbContextFactory();

            var categoryService = new WebApi.Services.CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.UpdateCategoryAsync(category);

            // Assert
            Assert.AreEqual(400, res.StatusCode);
        }

        [Test]
        public async Task Delete_CategoryService_Success()
        {
            const int id = 1;
            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new WebApi.Services.CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.DeleteCategoryAsync(id);

            // Assert
            Assert.IsTrue(res.Data);
        }

        [Test]
        public async Task Delete_CategoryService_Failure()
        {
            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new WebApi.Services.CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.DeleteCategoryAsync(0);

            // Assert
            Assert.IsFalse(res.Data);
        }

        [Test]
        public async Task GetById_CategoryService_Success()
        {
            const int id = 1;

            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new WebApi.Services.CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.GetCategoryByIdAsync(id);

            // Assert
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public async Task GetById_CategoryService_Failure()
        {
            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new WebApi.Services.CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.GetCategoryByIdAsync(0);

            // Assert
            Assert.AreEqual(400, res.StatusCode);
        }

        [Test]
        public async Task GetAll_CategoryService_Success()
        {
            // Arrange
            var context = new FakeDbContextFactory();

            var categoryService = new WebApi.Services.CategoryService(context.DbContextFactory());
            // Act
            var res = await categoryService.GetCategoriesAsync();

            // Assert
            Assert.IsNotNull(res.Data);
            Assert.AreEqual(200, res.StatusCode);
        }
    }
}
