using System;
using System.Collections.Generic;
using System.Globalization;
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
                Amount = 50.00m,
                DateTime = DateTime.ParseExact("19501212", "yyyyMMdd", CultureInfo.InvariantCulture),
                Note = "Mock Note"
            },
            new Entry
            {
                Account = AccountMockData.Accounts.FirstOrDefault(),
                Category = CategoryMockData.Categories.FirstOrDefault(),
                Amount = 100.00m,
                DateTime = DateTime.ParseExact("25501212", "yyyyMMdd", CultureInfo.InvariantCulture),
                Note = "Lorem Ipsum"
            }
        };
    }
}