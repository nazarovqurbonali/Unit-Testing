using WebApi.Data.Entities;

namespace xUnitApp.FakeData;

public class CategoryFakeData
{
    public IEnumerable<Category> GetAllPost()
    {
        var posts = new List<Category>();
        posts.Add(new Category { Id = 1, Description = "test1", Name = "test1", CreatedAt = DateTimeOffset.UtcNow });
        posts.Add(new Category { Id = 2, Description = "test2", Name = "test2", CreatedAt = DateTimeOffset.UtcNow });
        posts.Add(new Category { Id = 3, Description = "test3", Name = "test3", CreatedAt = DateTimeOffset.UtcNow });
        posts.Add(new Category { Id = 4, Description = "test4", Name = "test4", CreatedAt = DateTimeOffset.UtcNow });
        posts.Add(new Category { Id = 5, Description = "test5", Name = "test5", CreatedAt = DateTimeOffset.UtcNow });
        return posts;
    }
}