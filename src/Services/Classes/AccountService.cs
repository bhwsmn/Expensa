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
    public class AccountService : BaseService<Account, AccountQueryModel>
    {
        private readonly MainDbContext _context;

        public AccountService(MainDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Account>> GetFilteredAsync(AccountQueryModel filterQuery)
        {
            if (filterQuery == default)
            {
                return new List<Account>();
            }

            var query = _context.Accounts
                .ConditionalWhere(
                    () => filterQuery.ApplicationUserId != default,
                    account => account.ApplicationUser.Id == filterQuery.ApplicationUserId
                )
                .ConditionalWhere(
                    () => filterQuery.Name != default,
                    account => account.Name.ToLower().Contains(filterQuery.Name.ToLower())
                );

            var accounts = await query.OrderBy(account => account.Name).ToListAsync();

            return accounts;
        }
    }
}