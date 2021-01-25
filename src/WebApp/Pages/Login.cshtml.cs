using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO.Input;

namespace WebApp.Pages
{
    [AllowAnonymous]
    public class Login : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [BindProperty]
        public UserLoginInputDto InputDto { get; set; }

        [BindProperty]
        public string SignInError { get; set; }

        public Login(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(
                userName: InputDto.Username,
                password: InputDto.Password,
                isPersistent: false,
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(InputDto.Username);
                Response.Cookies.Append("ISOCurrencySymbol", user.Preference.ISOCurrencySymbol);
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("SignInError", "Too many login attempts. Please try again later.");
                return Page();
            }

            ModelState.AddModelError("SignInError", "Username/Password is wrong.");
            return Page();
        }
    }
}