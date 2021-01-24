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
            var cultureName = CultureInfo.GetCultureInfo("en-GB").Name;
            var accounts = AccountMockData.Accounts;

            foreach (var account in accounts)
            {
                await repository.CreateAsync(account);
            }

            var noQueryResult = await repository.GetFilteredAsync(null);
            var queryWithUserIdResult =
                await repository.GetFilteredAsync(new AccountQueryModel {UserId = userId});
            var queryWithAccountNameResult =
                await repository.GetFilteredAsync(new AccountQueryModel {Name = accountName});
            var queryWithCultureNameResult =
                await repository.GetFilteredAsync(new AccountQueryModel {CultureName = cultureName});

            Assert.Empty(noQueryResult);
            Assert.Equal(accounts.Count(), queryWithUserIdResult.Count());
            Assert.Single(queryWithAccountNameResult);
            Assert.Single(queryWithCultureNameResult);
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
            var originalCultureName = account.CultureName;
            var updatedAccountName = "Updated Account Name";
            var updatedCultureName = CultureInfo.GetCultureInfo("en-CA").Name;

            var updateDictionary = new Dictionary<string, dynamic>
            {
                {nameof(account.Name), updatedAccountName},
                {nameof(account.CultureName), updatedCultureName}
            };

            await repository.UpdateAsync(account.Id, updateDictionary);

            Assert.NotEqual(originalAccountName, account.Name);
            Assert.NotEqual(originalCultureName, account.CultureName);
            Assert.Equal(updatedAccountName, account.Name);
            Assert.Equal(updatedCultureName, account.CultureName);
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