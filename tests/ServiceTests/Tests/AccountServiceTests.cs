using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Models.Query;
using ServiceTests.Helpers;
using ServiceTests.MockData;
using Xunit;

namespace ServiceTests.Tests
{
    public class AccountServiceTests : IServiceTests
    {
        [Fact]
        public async Task CreateAsync_Success()
        {
            var repository = new Bootstrap().GetService<Account, AccountQueryModel>();
            var account = AccountMockData.Accounts.FirstOrDefault();
            await repository.CreateAsync(account);

            Assert.NotEqual(Guid.Empty, account.Id);
        }

        [Fact]
        public async Task ExistsAsync_Success()
        {
            var repository = new Bootstrap().GetService<Account, AccountQueryModel>();
            var account = AccountMockData.Accounts.FirstOrDefault();
            await repository.CreateAsync(account);

            var exists = await repository.ExistsAsync(account.Id);

            Assert.True(exists);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            var repository = new Bootstrap().GetService<Account, AccountQueryModel>();
            var account = AccountMockData.Accounts.FirstOrDefault();
            await repository.CreateAsync(account);

            var accountFromRepository = await repository.GetByIdAsync(account.Id);

            Assert.Equal(account.Id, accountFromRepository.Id);
        }

        [Fact]
        public async Task GetFilteredAsync_Success()
        {
            var repository = new Bootstrap().GetService<Account, AccountQueryModel>();
            var userId = UserMockData.Users.FirstOrDefault().Id;
            var accountName = AccountMockData.Accounts.FirstOrDefault().Name;
            var accounts = AccountMockData.Accounts;

            foreach (var account in accounts)
            {
                await repository.CreateAsync(account);
            }

            var noQueryResult = await repository.GetFilteredAsync(null);
            var queryWithUserIdResult =
                await repository.GetFilteredAsync(new AccountQueryModel {ApplicationUserId = userId});
            var queryWithAccountNameResult =
                await repository.GetFilteredAsync(new AccountQueryModel {Name = accountName});

            Assert.Empty(noQueryResult);
            Assert.Equal(accounts.Count(), queryWithUserIdResult.Count());
            Assert.Single(queryWithAccountNameResult);
        }

        [Fact]
        public async Task GetCountAsync_Success()
        {
            var repository = new Bootstrap().GetService<Account, AccountQueryModel>();
            var accounts = AccountMockData.Accounts;

            foreach (var account in accounts)
            {
                await repository.CreateAsync(account);
            }

            var count = await repository.GetCountAsync();

            Assert.Equal(accounts.Count(), count);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            var repository = new Bootstrap().GetService<Account, AccountQueryModel>();
            var account = AccountMockData.Accounts.FirstOrDefault();
            await repository.CreateAsync(account);

            var originalAccountName = account.Name;
            var updatedAccountName = "Updated Account Name";
            var updatedCultureName = CultureInfo.GetCultureInfo("en-CA").Name;

            var updateDictionary = new Dictionary<string, dynamic>
            {
                {nameof(account.Name), updatedAccountName}
            };

            await repository.UpdateAsync(account.Id, updateDictionary);

            Assert.NotEqual(originalAccountName, account.Name);
            Assert.Equal(updatedAccountName, account.Name);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            var repository = new Bootstrap().GetService<Account, AccountQueryModel>();
            var account = AccountMockData.Accounts.FirstOrDefault();
            await repository.CreateAsync(account);

            var existsAfterCreation = await repository.ExistsAsync(account.Id);

            await repository.DeleteAsync(account.Id);

            var existsAfterDeletion = await repository.ExistsAsync(account.Id);

            Assert.True(existsAfterCreation);
            Assert.False(existsAfterDeletion);
        }
    }
}