using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ServiceTests.MockData
{
    public static class UserMockData
    {
        public static IEnumerable<IdentityUser> Users { get; set; } = new List<IdentityUser>
        {
            new IdentityUser {UserName = "JohnDoe", Email = "johndoe@email.com"}
        };
    }
}