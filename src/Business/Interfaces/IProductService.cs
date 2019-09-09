using System;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces
{
    public interface IProductService : IService<Product>, IDisposable
    {
    }
}
