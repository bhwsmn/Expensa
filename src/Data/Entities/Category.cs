using System;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IdentityUser User { get; set; }
    }
}