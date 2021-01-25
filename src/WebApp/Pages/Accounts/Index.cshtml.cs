using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO.Output;
using Models.Query;
using Services.Interfaces;

namespace WebApp.Pages.Accounts
{
    public class Index : PageModel
    {
        private readonly IBaseService<Account, AccountQueryModel> _accountService;
        private readonly IMapper _mapper;

        [BindProperty]
        public IEnumerable<AccountOutputDto> OutputDtos { get; set; }

        public Index(IBaseService<Account, AccountQueryModel> accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task OnGetAsync()
        {
            var accounts = await _accountService.GetFilteredAsync(new AccountQueryModel
            {
                ApplicationUserId = HttpContext.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
            });

            OutputDtos = _mapper.Map<List<AccountOutputDto>>(accounts);
        }
    }
}