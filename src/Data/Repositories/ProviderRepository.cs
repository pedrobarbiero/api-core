using System;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(StockContext db) : base(db)
        {
        }

        public async Task<Provider> GetProviderAddress(Guid providerId)
        {
            return await _dbSet.AsNoTracking()
                               .Include(p => p.Address)
                               .FirstOrDefaultAsync(p => p.Id == providerId);
        }

        public async Task<Provider> GetProviderAddressProduct(Guid providerId)
        {
            return await _dbSet.AsNoTracking()
                               .Include(p => p.Address)
                               .Include(p => p.Products)
                               .FirstOrDefaultAsync(p => p.Id == providerId);
        }
    }
}
