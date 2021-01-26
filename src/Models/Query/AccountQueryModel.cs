using System;

namespace Models.Query
{
    public class AccountQueryModel
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string Name { get; set; }
        public string CultureName { get; set; }
    }
}