using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Data.Entities;

namespace ServiceTests.MockData
{
    public static class AccountMockData
    {
        public static IEnumerable<Account> Accounts { get; set; } = new List<Account>
        {
            new Account
            {
                ApplicationUser = UserMockData.Users.FirstOrDefault(),
                Name = "Mock Account"
            },
            new Account
            {
                ApplicationUser = UserMockData.Users.FirstOrDefault()
            }
        };
    }
}