using System;
using Data.DbContexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Classes;
using Services.Interfaces;

namespace ServiceTests.Helpers
{
    public class Bootstrap
    {
        private readonly MainDbContext _context;
        
        public Bootstrap()
        {
            DbContextOptions<MainDbContext> options;
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            _context = new MainDbContext(options);
        }

        public IBaseService<TEntity, TQuery> GetService<TEntity, TQuery>() where TEntity : class where TQuery : class
        {
            if (typeof(TEntity) == typeof(Account))
            {
                return new AccountService(_context) as IBaseService<TEntity, TQuery>;
            }
            if (typeof(TEntity) == typeof(Category))
            {
                return new CategoryService(_context) as IBaseService<TEntity, TQuery>;
            }
            if (typeof(TEntity) == typeof(Entry))
            {
                return new EntryService(_context) as IBaseService<TEntity, TQuery>;
            }

            return default;
        }
    }
}