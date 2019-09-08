using System;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetProviderAddress(Guid providerId);
        Task<Provider> GetProviderAddressProduct(Guid providerId);
    }
}
