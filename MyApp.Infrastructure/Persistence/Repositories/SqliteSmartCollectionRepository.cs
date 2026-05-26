using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Persistence.Repositories
{
    public class SqliteSmartCollectionRepository : ISmartCollectionRepository
    {
        private readonly AppDbContext _context;

        public SqliteSmartCollectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SmartCollection?> GetByIdAsync(Guid id) => 
            await _context.SmartCollections.FindAsync(id);

        public async Task<IEnumerable<SmartCollection>> GetAllAsync() => 
            await _context.SmartCollections.ToListAsync();

        public async Task AddAsync(SmartCollection collection)
        {
            await _context.SmartCollections.AddAsync(collection);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SmartCollection collection)
        {
            _context.SmartCollections.Update(collection);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var collection = await _context.SmartCollections.FindAsync(id);
            if (collection != null)
            {
                _context.SmartCollections.Remove(collection);
                await _context.SaveChangesAsync();
            }
        }
    }
}
