using System.Collections.Generic;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ServiceTests.MockData
{
    public static class UserMockData
    {
        public static IEnumerable<ApplicationUser> Users { get; set; } = new List<ApplicationUser>
        {
            new ApplicationUser {UserName = "JohnDoe"}
        };
    }
}