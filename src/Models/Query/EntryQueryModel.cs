using System;

namespace Models.Query
{
    public class EntryQueryModel
    {
        public Guid AccountId { get; set; }
        public virtual Guid CategoryId { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public string Note { get; set; }
    }
}