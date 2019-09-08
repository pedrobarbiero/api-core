using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByProvider(Guid providerId);
        Task<IEnumerable<Product>> GetProductsProviders();
        Task<Product> GetProductProvider(Guid productId);


    }
}
