using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Constants;
using Models.DTO.Input;

namespace WebApp.Pages
{
    [AllowAnonymous]
    public class Register : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [BindProperty] 
        public UserRegisterInputDto InputDto { get; set; }
        
        public Register(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            if (!EnvironmentVariables.RegistrationEnabled)
            {
                return RedirectToPage("/Login");
            }
            
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var user = new ApplicationUser
            {
                UserName = InputDto.Username,
                Preference = new Preference
                {
                    ISOCurrencySymbol = InputDto.Currency
                }
            };
            var result = await _userManager.CreateAsync(user, InputDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.PasswordSignInAsync(user, InputDto.Password, false, false);
                Response.Cookies.Append("ISOCurrencySymbol", user.Preference.ISOCurrencySymbol);
                return RedirectToPage("/Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    error.Code == nameof(IdentityErrorDescriber.DuplicateUserName)
                        ? "InputDto.Username"
                        : "InputDto.Password",
                    error.Description);
            }

            return Page();
        }
    }
}