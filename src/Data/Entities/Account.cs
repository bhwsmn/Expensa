using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CultureName { get; set; }
        public virtual IdentityUser User { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}