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
        public async Task<bool> Add(Product obj)
        {
            if (ExecuteValidation(new ProductValidation(), obj))
                return await _repository.Add(obj);

            return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async Task<bool> Update(Product obj)
        {
            if (ExecuteValidation(new ProductValidation(), obj))
                return await _repository.Update(obj);

            return false;
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
