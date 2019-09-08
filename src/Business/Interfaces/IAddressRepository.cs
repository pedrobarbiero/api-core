using System;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetByProvider(Guid providerId);
    }
}
