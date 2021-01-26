using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO.Input;
using Models.DTO.Output;
using Models.Query;
using Services.Interfaces;

namespace WebApp.Pages.Entries
{
    public class Create : PageModel
    {
        private readonly IBaseService<Entry, EntryQueryModel> _entryService;
        private readonly IBaseService<Account, AccountQueryModel> _accountService;
        private readonly IBaseService<Category, CategoryQueryModel> _categoryService;
        private readonly IMapper _mapper;

        [BindProperty] 
        public EntryInputDto InputDto { get; set; }
        [BindProperty] 
        public IEnumerable<AccountOutputDto> AccountOutputDtos { get; set; }
        [BindProperty] 
        public IEnumerable<CategoryOutputDto> CategoryOutputDtos { get; set; }

        public Create(
            IBaseService<Entry, EntryQueryModel> entryService,
            IBaseService<Account, AccountQueryModel> accountService,
            IBaseService<Category, CategoryQueryModel> categoryService,
            IMapper mapper
        )
        {
            _entryService = entryService;
            _accountService = accountService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task OnGetAsync()
        {
            var applicationUserId = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var accounts = await _accountService.GetFilteredAsync(
                new AccountQueryModel
                {
                    ApplicationUserId = applicationUserId
                });
            var categories = await _categoryService.GetFilteredAsync(
                new CategoryQueryModel
                {
                    ApplicationUserId = applicationUserId
                });

            AccountOutputDtos = _mapper.Map<List<AccountOutputDto>>(accounts);
            CategoryOutputDtos = _mapper.Map<List<CategoryOutputDto>>(categories);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var entry = _mapper.Map<Entry>(InputDto);
            entry.ApplicationUserId = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            await _entryService.CreateAsync(entry);

            return RedirectToPage("/Entries/Index");
        }
    }
}