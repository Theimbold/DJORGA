using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Persistence
{
    public interface ISmartCollectionRepository
    {
        Task<SmartCollection?> GetByIdAsync(Guid id);
        Task<IEnumerable<SmartCollection>> GetAllAsync();
        Task AddAsync(SmartCollection collection);
        Task UpdateAsync(SmartCollection collection);
        Task DeleteAsync(Guid id);
    }
}
