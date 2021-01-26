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

namespace WebApp.Pages.Categories
{
    public class Edit : PageModel
    {
        private readonly IBaseService<Category, CategoryQueryModel> _categoryService;
        private readonly IMapper _mapper;

        [BindProperty] 
        public CategoryInputDto InputDto { get; set; }

        public Edit(IBaseService<Category, CategoryQueryModel> category, IMapper mapper)
        {
            _categoryService = category;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var category = (await _categoryService.GetFilteredAsync(
                new CategoryQueryModel
                {
                    Id = Guid.Parse(RouteData.Values["id"]?.ToString()),
                    ApplicationUserId = HttpContext.User.Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
                })).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            InputDto = _mapper.Map<CategoryInputDto>(category);

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
            await _categoryService.UpdateAsync(id, updateDictionary);

            return RedirectToPage("/Categories/Edit", new {id});
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var id = Guid.Parse(RouteData.Values["id"].ToString());
            await _categoryService.DeleteAsync(id);

            return RedirectToPage("/Categories/Index");
        }
    }
}