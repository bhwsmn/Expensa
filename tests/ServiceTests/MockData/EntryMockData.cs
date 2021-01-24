using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;

namespace ServiceTests.MockData
{
    public static class EntryMockData
    {
        public static IEnumerable<Entry> Entries { get; set; } = new List<Entry>
        {
            new Entry
            {
                Account = AccountMockData.Accounts.FirstOrDefault(),
                Category = CategoryMockData.Categories.FirstOrDefault(),
                Amount = 500.00m,
                DateTime = DateTime.Parse("19501212")
            },
            new Entry
            {
                Account = AccountMockData.Accounts.FirstOrDefault(),
                Category = CategoryMockData.Categories.FirstOrDefault(),
                Amount = 800.00m,
                DateTime = DateTime.Parse("20501212")
            }
        };
    }
}