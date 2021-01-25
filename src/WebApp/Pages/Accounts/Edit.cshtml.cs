using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO.Input;

namespace WebApp.Pages.Accounts
{
    public class Edit : PageModel
    {
        [BindProperty]
        public AccountInputDto InputDto { get; set; }
        public void OnGet()
        {
            
        }
    }
}