using System;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces
{
    public interface IProviderService : IService<Provider>, IDisposable
    {
        Task UpdateAddress(Address address);
    }
}
