using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO.Input;
using Models.Query;
using Services.Interfaces;

namespace WebApp.Pages.Accounts
{
    public class Create : PageModel
    {
        private readonly IBaseService<Account, AccountQueryModel> _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        [BindProperty] public AccountInputDto InputDto { get; set; }

        public Create(
            IBaseService<Account, AccountQueryModel> accountService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper
        )
        {
            _accountService = accountService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var account = _mapper.Map<Account>(InputDto);
            account.ApplicationUserId = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            await _accountService.CreateAsync(account);

            return RedirectToPage("/Accounts/Index");
        }
    }
}