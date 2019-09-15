using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DataTransferObjects;
using WebApi.Extensions;

namespace WebApi.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProvidersController : MainController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IProviderService _service;
        public ProvidersController(IProviderRepository repository,
                                   IMapper mapper,
                                   IProviderService service,
                                   IAddressRepository addressRepository,
                                   INotifier notifier,
                                   IUser user) : base(notifier, user)
        {
            _providerRepository = repository;
            _mapper = mapper;
            _service = service;
            _addressRepository = addressRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProviderDTO>> GetAll()
        {
            List<Provider> providers = await _providerRepository.GetAll();
            return _mapper.Map<IEnumerable<ProviderDTO>>(providers);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProviderDTO>> Get(Guid id)
        {
            Provider provider = await _providerRepository.GetProviderAddressProduct(id);

            if (provider == null)
                return NotFound();

            return Ok(_mapper.Map<ProviderDTO>(provider));
        }

        [HttpPost]
        [ClaimsAuthorize("Provider", "Create")]
        public async Task<ActionResult<ProviderDTO>> Create(ProviderDTO providerDTO)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var provider = _mapper.Map<Provider>(providerDTO);
            await _service.Add(provider);

            return CustomResponse(providerDTO);
        }

        [HttpGet("get-address/{id:guid}")]
        public async Task<ActionResult<AddressDTO>> GetAddresById(Guid id)
        {
            var addressDTO = _mapper.Map<AddressDTO>(await _addressRepository.GetById(id));
            return Ok(addressDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        [ClaimsAuthorize("Provider", "Update")]
        public async Task<ActionResult<ProviderDTO>> Update(Guid id, ProviderDTO providerDTO)
        {
            if (id != providerDTO.Id)
            {
                NotifyError($"O id do objeto({providerDTO.Id}) é diferente do informado({id}).");
                return CustomResponse(providerDTO);
            }

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var provider = _mapper.Map<Provider>(providerDTO);
            await _service.Update(provider);

            return CustomResponse(providerDTO);
        }

        [HttpPut("update-addres/{id:guid}")]
        [ClaimsAuthorize("Provider", "Update")]
        public async Task<ActionResult> UpdateAddress(Guid id, AddressDTO addressDTO)
        {
            if (id != addressDTO.Id)
            {
                NotifyError($"O id do objeto({addressDTO.Id}) é diferente do informado({id}).");
                return CustomResponse(addressDTO);
            }

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var address = _mapper.Map<Address>(addressDTO);
            await _service.UpdateAddress(address);

            return CustomResponse(addressDTO);
        }

        [HttpDelete("{id:guid}")]
        [ClaimsAuthorize("Provider", "Delete")]

        public async Task<ActionResult<ProviderDTO>> Delete(Guid id)
        {
            var provider = _providerRepository.GetById(id);
            if (provider == null)
                return NotFound();

            await _service.Delete(id);
            return CustomResponse();
        }

    }
}