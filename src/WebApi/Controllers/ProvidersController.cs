using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataTransferObjects;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProvidersController : MainController
    {
        private readonly IProviderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProviderService _service;
        public ProvidersController(IProviderRepository repository,
                                   IMapper mapper,
                                   IProviderService service,
                                   INotifier notifier) : base(notifier)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<ProviderDTO>> GetAll()
        {
            List<Provider> providers = await _repository.GetAll();
            return _mapper.Map<IEnumerable<ProviderDTO>>(providers);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProviderDTO>> Get(Guid id)
        {
            Provider provider = await _repository.GetProviderAddressProduct(id);

            if (provider == null)
                return NotFound();

            return Ok(_mapper.Map<ProviderDTO>(provider));
        }

        [HttpPost]
        public async Task<ActionResult<ProviderDTO>> Create(ProviderDTO providerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var provider = _mapper.Map<Provider>(providerDTO);
            var result = await _service.Add(provider);

            if (!result)
                return BadRequest();

            return Ok(provider);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<ProviderDTO>> Update(Guid id, ProviderDTO providerDTO)
        {
            if (id != providerDTO.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var provider = _mapper.Map<Provider>(providerDTO);
            var result = await _service.Update(provider);

            if (!result)
                return BadRequest();

            return Ok(provider);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProviderDTO>> Delete(Guid id)
        {
            var provider = _repository.GetById(id);
            if (provider == null)
                return NotFound();

            await _service.Delete(id);
            return Ok(provider);
        }

    }
}