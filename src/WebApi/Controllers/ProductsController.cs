using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataTransferObjects;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(INotifier notifier, IProductRepository productRepository, IMapper mapper, IProductService productService) : base(notifier)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> Get(Guid id)
        {
            var product = await _productRepository.GetProductProvider(id);
            var productDto = _mapper.Map<ProductDTO>(product);
            return CustomResponse(productDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            var products = await _productRepository.GetAll();
            return CustomResponse(_mapper.Map<IEnumerable<ProductDTO>>(products));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            await _productService.Delete(id);
            return CustomResponse();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var imageName = $"{Guid.NewGuid()}_{productDTO.Image}";

            if (!UploadFile(productDTO.ImageUpload, imageName))
                return CustomResponse(productDTO);

            productDTO.Image = imageName;
            Product product = _mapper.Map<Product>(productDTO);
            product.Provider = null;
            await _productService.Add(product);

            return CustomResponse(productDTO);
        }

        private bool UploadFile(string file, string imagename)
        {
            if (string.IsNullOrEmpty(file))
            {
                NotifyError("Imagem obrigatória!");
                return false;
            }
            var imageDataByteArray = Convert.FromBase64String(file);

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imagename);
            if (System.IO.File.Exists(filepath))
            {
                NotifyError("Imagem já cadastrada");
                return false;
            }

            System.IO.File.WriteAllBytes(filepath, imageDataByteArray);
            return true;
        }


    }
}