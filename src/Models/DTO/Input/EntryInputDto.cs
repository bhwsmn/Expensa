using System;

namespace Models.DTO.Input
{
    public class EntryInputDto
    {
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public virtual Guid CategoryId { get; set; }
    }
}