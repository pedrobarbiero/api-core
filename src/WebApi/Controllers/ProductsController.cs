using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataTransferObjects;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(INotifier notifier,
            IProductRepository productRepository,
            IMapper mapper,
            IProductService productService,
            IUser user) : base(notifier, user)
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
        [AllowAnonymous]
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

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> Update(Guid id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                NotifyError("O Id do objeto é diferente do informado");
                return CustomResponse(productDTO);
            }

            var productUpdate = await _productRepository.GetById(id);
            productDTO.Image = productUpdate.Image;

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            if (productDTO.ImageUploadFile != null)
            {
                var imgName = Guid.NewGuid() + "_" + productDTO.Image;
                if (!await UploadFile(productDTO.ImageUploadFile, imgName))
                    return CustomResponse(ModelState);

                productUpdate.Image = imgName;
            }

            productUpdate.Name = productDTO.Name;
            productUpdate.Description = productDTO.Description;
            productUpdate.Value = productDTO.Value;
            productUpdate.Active = productDTO.Active;

            await _productService.Update(productUpdate);
            return CustomResponse(productDTO);
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

        [HttpPost("product-image")]
        public async Task<ActionResult<ProductDTO>> CreateProductImage(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var prefix = Guid.NewGuid() + "_";

            if (!await UploadFile(productDTO.ImageUploadFile, prefix))
                return CustomResponse();

            productDTO.Image = prefix + productDTO.ImageUploadFile.FileName;
            await _productService.Add(_mapper.Map<Product>(productDTO));

            return CustomResponse();
        }

        private async Task<bool> UploadFile(IFormFile file, string prefix)
        {
            if (file == null || file.Length == 0)
            {
                NotifyError("Imagem obrigatória");
                return false;
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", prefix + file.FileName);
            if (System.IO.File.Exists(path))
            {
                NotifyError("Imagem já cadastrada");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
                await file.CopyToAsync(stream);

            return true;
        }

    }
}