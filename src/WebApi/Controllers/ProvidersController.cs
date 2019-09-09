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
        public ProvidersController(IProviderRepository repository, IMapper mapper, IProviderService service)
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
            Provider provider = await _repository.GetById(id);

            if (provider == null)
                return NotFound();

            return Ok(_mapper.Map<ProviderDTO>(provider));
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProviderDTO providerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var provider = _mapper.Map<Provider>(providerDTO);
            await _service.Add(provider);
            return Ok();
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, ProviderDTO providerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var provider = _mapper.Map<Provider>(providerDTO);
            await _service.Update(provider);
            return Ok();
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok();
        }

    }
}