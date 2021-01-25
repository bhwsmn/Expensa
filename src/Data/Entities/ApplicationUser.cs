using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public virtual Preference Preference { get; set; }
    }
}