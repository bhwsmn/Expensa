using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO.Input;
using Models.Query;
using Services.Interfaces;

namespace WebApp.Pages.Accounts
{
    public class Edit : PageModel
    {
        private readonly IBaseService<Account, AccountQueryModel> _accountService;
        private readonly IMapper _mapper;

        [BindProperty] 
        public AccountInputDto InputDto { get; set; }

        public Edit(IBaseService<Account, AccountQueryModel> accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var account = (await _accountService.GetFilteredAsync(
                new AccountQueryModel
                {
                    Id = Guid.Parse(RouteData.Values["id"]?.ToString()),
                    ApplicationUserId = HttpContext.User.Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
                })).FirstOrDefault();

            if (account == null)
            {
                return NotFound();
            }

            InputDto = _mapper.Map<AccountInputDto>(account);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var id = Guid.Parse(RouteData.Values["id"].ToString());
            var updateDictionary = new Dictionary<string, dynamic> {{nameof(Account.Name), InputDto.Name}};
            await _accountService.UpdateAsync(id, updateDictionary);

            return RedirectToPage("/Accounts/Edit", new {id});
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var id = Guid.Parse(RouteData.Values["id"].ToString());
            await _accountService.DeleteAsync(id);

            return RedirectToPage("/Accounts/Index");
        }
    }
}