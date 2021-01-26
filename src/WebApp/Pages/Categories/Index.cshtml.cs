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

namespace WebApp.Pages.Categories
{
    public class Index : PageModel
    {
        private readonly IBaseService<Category, CategoryQueryModel> _categoryService;
        private readonly IMapper _mapper;

        [BindProperty]
        public IEnumerable<CategoryOutputDto> OutputDtos { get; set; }

        public Index(IBaseService<Category, CategoryQueryModel> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task OnGetAsync()
        {
            var categories = await _categoryService.GetFilteredAsync(
                new CategoryQueryModel
            {
                ApplicationUserId = HttpContext.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
            });

            OutputDtos = _mapper.Map<List<CategoryOutputDto>>(categories);
        }
    }
}