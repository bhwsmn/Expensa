using System;

namespace Models.Query
{
    public class CategoryQueryModel
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string Name { get; set; }
    }
}