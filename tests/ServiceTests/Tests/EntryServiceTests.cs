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
    public class EntryServiceTests : IServiceTests
    {
        [Fact]
        public async Task CreateAsync_Success()
        {
            var repository = new Bootstrap().GetService<Entry, EntryQueryModel>();
            var entry = EntryMockData.Entries.FirstOrDefault();
            await repository.CreateAsync(entry);

            Assert.NotEqual(Guid.Empty, entry.Id);
        }

        [Fact]
        public async Task ExistsAsync_Success()
        {
            var repository = new Bootstrap().GetService<Entry, EntryQueryModel>();
            var entry = EntryMockData.Entries.FirstOrDefault();
            await repository.CreateAsync(entry);

            var exists = await repository.ExistsAsync(entry.Id);

            Assert.True(exists);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            var repository = new Bootstrap().GetService<Entry, EntryQueryModel>();
            var entry = EntryMockData.Entries.FirstOrDefault();
            await repository.CreateAsync(entry);

            var entryFromRepository = await repository.GetByIdAsync(entry.Id);

            Assert.Equal(entry.Id, entryFromRepository.Id);
        }

        [Fact]
        public async Task GetFilteredAsync_Success()
        {
            var repository = new Bootstrap().GetService<Entry, EntryQueryModel>();
            var accountId = AccountMockData.Accounts.FirstOrDefault().Id;
            var categoryId = CategoryMockData.Categories.FirstOrDefault().Id;
            var minAmountLow = 50.00m;
            var maxAmountLow = 51.00m;
            var minAmountHigh = 100.00m;
            var maxAmountHigh = 101.00m;
            var fromDateTimeLow = DateTime.MinValue;
            var toDateTimeLow = DateTime.UtcNow;
            var fromDateTimeHigh = DateTime.UtcNow;
            var toDateTimeHigh = DateTime.MaxValue;
            var note = "Mock";
            var entries = EntryMockData.Entries;

            foreach (var account in entries)
            {
                await repository.CreateAsync(account);
            }

            var noQueryResult = await repository.GetFilteredAsync(null);
            var queryWithAccountId =
                await repository.GetFilteredAsync(new EntryQueryModel {AccountId = accountId});
            var queryWithCategoryId =
                await repository.GetFilteredAsync(new EntryQueryModel {CategoryId = categoryId});
            var queryWithLowAmount =
                await repository.GetFilteredAsync(new EntryQueryModel
                {
                    MinAmount = minAmountLow, 
                    MaxAmount = maxAmountLow
                });
            var queryWithHighAmount =
                await repository.GetFilteredAsync(new EntryQueryModel
                {
                    MinAmount = minAmountHigh, 
                    MaxAmount = maxAmountHigh
                });
            var queryWithLowDateTime =
                await repository.GetFilteredAsync(new EntryQueryModel
                {
                    FromDateTime = fromDateTimeLow,
                    ToDateTime = toDateTimeLow
                });
            var queryWithHighDateTime =
                await repository.GetFilteredAsync(new EntryQueryModel
                {
                    FromDateTime = fromDateTimeHigh,
                    ToDateTime = toDateTimeHigh
                });
            var queryWithNote =
                await repository.GetFilteredAsync(new EntryQueryModel {Note = note});

            Assert.Empty(noQueryResult);
            Assert.Equal(entries.Count(), queryWithAccountId.Count());
            Assert.Equal(entries.Count(), queryWithCategoryId.Count());
            Assert.Single(queryWithLowAmount);
            Assert.Single(queryWithHighAmount);
            Assert.Single(queryWithLowDateTime);
            Assert.Single(queryWithHighDateTime);
            Assert.Single(queryWithNote);
        }

        [Fact]
        public async Task GetCountAsync_Success()
        {
            var repository = new Bootstrap().GetService<Entry, EntryQueryModel>();
            var entries = EntryMockData.Entries;

            foreach (var entry in entries)
            {
                await repository.CreateAsync(entry);
            }

            var count = await repository.GetCountAsync();

            Assert.Equal(entries.Count(), count);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            var repository = new Bootstrap().GetService<Entry, EntryQueryModel>();
            var entry = EntryMockData.Entries.FirstOrDefault();
            await repository.CreateAsync(entry);

            var originalEntryAmount = entry.Amount;
            var originalEntryDateTime = entry.DateTime;
            var originalEntryNote = entry.Note;
            var originalEntryCategory = entry.Category;
            var updatedEntryAmount = 2500.00m;
            var updatedEntryDateTime = DateTime.UtcNow;
            var updatedEntryNote = "Updated Note";
            var updatedEntryCategory = CategoryMockData.Categories.LastOrDefault();

            var updateDictionary = new Dictionary<string, dynamic>
            {
                {nameof(entry.Amount), updatedEntryAmount},
                {nameof(entry.DateTime), updatedEntryDateTime},
                {nameof(entry.Note), updatedEntryNote},
                {nameof(entry.Category), updatedEntryCategory}
            };

            await repository.UpdateAsync(entry.Id, updateDictionary);

            Assert.NotEqual(originalEntryAmount, entry.Amount);
            Assert.NotEqual(originalEntryDateTime, entry.DateTime);
            Assert.NotEqual(originalEntryNote, entry.Note);
            Assert.NotEqual(originalEntryCategory, entry.Category);
            Assert.Equal(updatedEntryAmount, entry.Amount);
            Assert.Equal(updatedEntryDateTime, entry.DateTime);
            Assert.Equal(updatedEntryNote, entry.Note);
            Assert.Equal(updatedEntryCategory, entry.Category);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            var repository = new Bootstrap().GetService<Entry, EntryQueryModel>();
            var entry = EntryMockData.Entries.FirstOrDefault();
            await repository.CreateAsync(entry);

            var existsAfterCreation = await repository.ExistsAsync(entry.Id);

            await repository.DeleteAsync(entry.Id);

            var existsAfterDeletion = await repository.ExistsAsync(entry.Id);

            Assert.True(existsAfterCreation);
            Assert.False(existsAfterDeletion);
        }
    }
}