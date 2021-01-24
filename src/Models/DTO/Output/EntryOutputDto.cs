using System;

namespace Models.DTO.Output
{
    public class EntryOutputDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public CategoryOutputDto Category { get; set; }
    }
}