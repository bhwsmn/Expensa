using System.Collections.Generic;
using System.Linq;
using Data.Entities;

namespace ServiceTests.MockData
{
    public static class CategoryMockData
    {
        public static IEnumerable<Category> Categories { get; set; } = new List<Category>
        {
            new Category
            {
                User = UserMockData.Users.FirstOrDefault(),
                Name = "Mock Category"
            },
            new Category
            {
                User = UserMockData.Users.FirstOrDefault(),
                Name = "Second Mock Category"
            },
        };
    }
}