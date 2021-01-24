using System;

namespace Models.DTO.Output
{
    public class AccountOutputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CultureName { get; set; }
    }
}