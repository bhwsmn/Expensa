using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Classes
{
    public class BaseService<TEntity, TQueryModel>: IBaseService<TEntity, TQueryModel> where TEntity: class where TQueryModel: class
    {
        private readonly MainDbContext _context;

        public BaseService(MainDbContext context)
        {
            _context = context;
        }
        
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            var exists = await _context.Set<TEntity>().FindAsync(id);

            return exists != null;
        }
        
        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync(TQueryModel filterQuery)
        {
            // Needs to be implemented manually for each Entity type
            throw new NotImplementedException();
        }

        public virtual async Task<int> GetCountAsync()
        {
            var count = await _context.Set<TEntity>().CountAsync();

            return count;
        }

        public virtual async Task<TEntity> UpdateAsync(Guid id, Dictionary<string, dynamic> updateDictionary)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            var type = typeof(TEntity);
            
            foreach (var property in type.GetProperties())
            {
                if (updateDictionary.ContainsKey(property.Name))
                {
                        property.SetValue(entity, updateDictionary[property.Name]);
                }
            }
            
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}