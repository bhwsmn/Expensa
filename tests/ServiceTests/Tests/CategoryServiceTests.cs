using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Models.Query;
using ServiceTests.Helpers;
using ServiceTests.MockData;
using Xunit;

namespace ServiceTests.Tests
{
    public class CategoryServiceTests : IServiceTests
    {
        [Fact]
        public async Task CreateAsync_Success()
        {
            var repository = new Bootstrap().GetService<Category, CategoryQueryModel>();
            var category = CategoryMockData.Categories.FirstOrDefault();
            await repository.CreateAsync(category);

            Assert.NotEqual(Guid.Empty, category.Id);
        }

        [Fact]
        public async Task ExistsAsync_Success()
        {
            var repository = new Bootstrap().GetService<Category, CategoryQueryModel>();
            var category = CategoryMockData.Categories.FirstOrDefault();
            await repository.CreateAsync(category);

            var exists = await repository.ExistsAsync(category.Id);

            Assert.True(exists);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            var repository = new Bootstrap().GetService<Category, CategoryQueryModel>();
            var category = CategoryMockData.Categories.FirstOrDefault();
            await repository.CreateAsync(category);

            var categoryFromRepository = await repository.GetByIdAsync(category.Id);

            Assert.Equal(category.Id, categoryFromRepository.Id);
        }

        [Fact]
        public async Task GetFilteredAsync_Success()
        {
            var repository = new Bootstrap().GetService<Category, CategoryQueryModel>();
            var userId = UserMockData.Users.FirstOrDefault().Id;
            var categoryName = CategoryMockData.Categories.FirstOrDefault().Name;
            var categories = CategoryMockData.Categories;

            foreach (var category in categories)
            {
                await repository.CreateAsync(category);
            }

            var noQueryResult = await repository.GetFilteredAsync(null);
            var queryWithUserIdResult =
                await repository.GetFilteredAsync(new CategoryQueryModel {ApplicationUserId = userId});
            var queryWithAccountNameResult =
                await repository.GetFilteredAsync(new CategoryQueryModel {Name = categoryName});

            Assert.Empty(noQueryResult);
            Assert.Equal(categories.Count(), queryWithUserIdResult.Count());
            Assert.Single(queryWithAccountNameResult);
        }

        [Fact]
        public async Task GetCountAsync_Success()
        {
            var repository = new Bootstrap().GetService<Category, CategoryQueryModel>();
            var categories = CategoryMockData.Categories;

            foreach (var category in categories)
            {
                await repository.CreateAsync(category);
            }

            var count = await repository.GetCountAsync();

            Assert.Equal(categories.Count(), count);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            var repository = new Bootstrap().GetService<Category, CategoryQueryModel>();
            var category = CategoryMockData.Categories.FirstOrDefault();
            await repository.CreateAsync(category);

            var originalCategoryName = category.Name;
            var updatedCategoryName = "Updated Category Name";

            var updateDictionary = new Dictionary<string, dynamic>
            {
                {nameof(category.Name), updatedCategoryName}
            };

            await repository.UpdateAsync(category.Id, updateDictionary);

            Assert.NotEqual(originalCategoryName, category.Name);
            Assert.Equal(updatedCategoryName, category.Name);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            var repository = new Bootstrap().GetService<Category, CategoryQueryModel>();
            var account = CategoryMockData.Categories.FirstOrDefault();
            await repository.CreateAsync(account);

            var existsAfterCreation = await repository.ExistsAsync(account.Id);

            await repository.DeleteAsync(account.Id);

            var existsAfterDeletion = await repository.ExistsAsync(account.Id);

            Assert.True(existsAfterCreation);
            Assert.False(existsAfterDeletion);
        }
    }
}