using System;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Business.Validations;

namespace Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository productRepository, 
                INotifier notifier) : base(notifier)
        {
            _repository = productRepository;
        }
        public async Task Add(Product obj)
        {
            if (ExecuteValidation(new ProductValidation(), obj))
                await _repository.Add(obj);
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }

        public async Task Update(Product obj)
        {
            if (ExecuteValidation(new ProductValidation(), obj))
                await _repository.Update(obj);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
