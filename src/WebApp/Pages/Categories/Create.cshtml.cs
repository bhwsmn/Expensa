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

namespace WebApp.Pages.Categories
{
    public class Create : PageModel
    {
        private readonly IBaseService<Category, CategoryQueryModel> _categoryService;
        private readonly IMapper _mapper;

        [BindProperty] 
        public CategoryInputDto InputDto { get; set; }

        public Create(IBaseService<Category, CategoryQueryModel> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var category = _mapper.Map<Category>(InputDto);
            category.ApplicationUserId = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            await _categoryService.CreateAsync(category);

            return RedirectToPage("/Categories/Index");
        }
    }
}