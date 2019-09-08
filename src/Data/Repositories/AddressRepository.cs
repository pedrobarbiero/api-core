using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(StockContext db) : base(db)
        {
        }

        public async Task<Address> GetByProvider(Guid providerId)
        {
            return await _dbSet.AsNoTracking()
                        .FirstOrDefaultAsync(a => a.ProviderId == providerId);
        }
    }
}
