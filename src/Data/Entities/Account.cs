using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
    }
}