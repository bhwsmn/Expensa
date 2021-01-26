using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DbContexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Query;
using Services.Helpers;

namespace Services.Classes
{
    public class CategoryService: BaseService<Category, CategoryQueryModel>
    {
        private readonly MainDbContext _context;

        public CategoryService(MainDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Category>> GetFilteredAsync(CategoryQueryModel filterQuery)
        {
            if (filterQuery == default)
            {
                return new List<Category>();
            }

            var query = _context.Categories
                .ConditionalWhere(
                    () => filterQuery.Id != default,
                    category => category.Id == filterQuery.Id
                )
                .ConditionalWhere(
                    () => filterQuery.ApplicationUserId != default,
                    category => category.ApplicationUser.Id == filterQuery.ApplicationUserId
                )
                .ConditionalWhere(
                    () => filterQuery.Name != default,
                    category => category.Name.ToLower().Contains(filterQuery.Name.ToLower())
                );

            var categories = await query.OrderBy(category => category.Name).ToListAsync();

            return categories;
        }
    }
}