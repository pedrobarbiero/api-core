using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(StockContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Product>> GetByProvider(Guid providerId)
        {
            return await Get(p => p.ProviderId == providerId);
        }

        public async Task<Product> GetProductProvider(Guid productId)
        {
            return await _dbSet.AsNoTracking()
                   .Include(p => p.Provider)
                   .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetProductsProviders()
        {
            return await _dbSet.AsNoTracking()
                .Include(p => p.Provider)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
