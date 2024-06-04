using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Entities;

namespace NUnitApp.FakeDbContext;

public class FakeDbContextFactory
{
    public DataContext DbContextFactory()
    {
        var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("InMem").Options;

        var context = new DataContext(options);

        context.Database.EnsureCreated();
        context.AddRange(
            new Category { Id = 1, Description = "test1", Name = "test1", CreatedAt = DateTimeOffset.UtcNow },
            new Category { Id = 2, Description = "test2", Name = "test2", CreatedAt = DateTimeOffset.UtcNow },
            new Category { Id = 3, Description = "test3", Name = "test3", CreatedAt = DateTimeOffset.UtcNow },
            new Category { Id = 4, Description = "test4", Name = "test4", CreatedAt = DateTimeOffset.UtcNow },
            new Category { Id = 5, Description = "test5", Name = "test5", CreatedAt = DateTimeOffset.UtcNow });
        context.SaveChanges();
        return context;
    }
}