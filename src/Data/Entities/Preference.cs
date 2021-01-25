using System;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class Preference
    {
        public Guid Id { get; set; }
        public string ISOCurrencySymbol { get; set; }
    }
}