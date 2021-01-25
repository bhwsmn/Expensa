using System;

namespace Data.Entities
{
    public class Entry
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public virtual Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Account Account { get; set; }
    }
}