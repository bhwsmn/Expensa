using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBaseService<TEntity, in TQueryModel> where TEntity: class where TQueryModel: class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> ExistsAsync(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetFilteredAsync(TQueryModel filterQuery);
        Task<int> GetCountAsync();
        Task<TEntity> UpdateAsync(Guid id, Dictionary<string, dynamic> updateDictionary);
        Task DeleteAsync(Guid id);
    }
}