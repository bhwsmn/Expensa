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
    public class EntryService : BaseService<Entry, EntryQueryModel>
    {
        private readonly MainDbContext _context;

        public EntryService(MainDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Entry>> GetFilteredAsync(EntryQueryModel filterQuery)
        {
            if (filterQuery == default)
            {
                return new List<Entry>();
            }

            var query = _context.Entries
                .ConditionalWhere(
                    () => filterQuery.AccountId != default,
                    entry => entry.Account.Id == filterQuery.AccountId
                )
                .ConditionalWhere(
                    () => filterQuery.CategoryId != default,
                    entry => entry.Category.Id == filterQuery.CategoryId
                )
                .ConditionalWhere(
                    () => filterQuery.MinAmount != default,
                    entry => entry.Amount >= filterQuery.MinAmount
                )
                .ConditionalWhere(
                    () => filterQuery.MaxAmount != default,
                    entry => entry.Amount <= filterQuery.MaxAmount
                )
                .ConditionalWhere(
                    () => filterQuery.FromDateTime != default,
                    entry => entry.DateTime >= filterQuery.FromDateTime
                )
                .ConditionalWhere(
                    () => filterQuery.ToDateTime != default,
                    entry => entry.DateTime <= filterQuery.ToDateTime
                )
                .ConditionalWhere(
                    () => filterQuery.Note != default,
                    entry => entry.Note.ToLower().Contains(filterQuery.Note.ToLower())
                );

            var entries = await query.OrderByDescending(entry => entry.DateTime).ToListAsync();

            return entries;
        }
    }
}