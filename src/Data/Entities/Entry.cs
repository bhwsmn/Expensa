using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Entry
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        
        public virtual Guid CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
        
        public virtual Guid AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public virtual Account Account { get; set; }
        
        public virtual string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}