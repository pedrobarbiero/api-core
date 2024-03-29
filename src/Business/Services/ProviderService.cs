using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Business.Validations;

namespace Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {

        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository,
            IAddressRepository addressRepository,
            INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }


        public async Task<bool> Add(Provider provider)
        {

            if (!ExecuteValidation(new ProviderValidation(), provider)
            && !ExecuteValidation(new AddressValidation(), provider.Address))
                return false;

            var providerDocument = await _providerRepository.Get(p => p.Document == provider.Document);
            if (providerDocument.Any())
            {
                Notify($"Já existe o documento {provider.Document} cadastrado.");
                return false;
            }

            await _providerRepository.Add(provider);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var providerProducts = await _providerRepository.GetProviderAddressProduct(id);
            if (providerProducts.Products.Any())
            {
                Notify("O Fornecedor possui produtos cadastrados");
                return false;
            }

            await _providerRepository.Delete(id);
            return true;
        }

        public async Task<bool> Update(Provider provider)
        {
            var providerDocument = await _providerRepository.Get(p => p.Document == provider.Document
                && p.Id != provider.Id);

            if (providerDocument.Any())
            {
                Notify($"Já existe o documento {provider.Document} cadastrado.");
                return false;
            }

            await _providerRepository.Update(provider);
            return true;
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);
        }

        public void Dispose()
        {
            _addressRepository?.Dispose();
            _providerRepository?.Dispose();
        }
    }
}
