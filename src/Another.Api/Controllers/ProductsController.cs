using Another.Api.Dtos;
using Another.Business.Interfaces;
using Another.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Another.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository,
            IProductService productService,
            ISupplierRepository supplierRepository,
            IMapper mapper,
            INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
            _productService = productService;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = _mapper.Map<ProductDto>(await _productRepository.GetById(id));

            if (product == null) NotFound("Product not found");

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetProductsSuppliers());
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Add(ProductDto productDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = Guid.NewGuid() + "-" + productDto.Image;

            productDto.Image = imageName;

            if(!UploadImage(productDto.ImageUpload, imageName))
            {
                return CustomResponse();
            }

            await _productService.Add(_mapper.Map<Product>(productDto));

            return CustomResponse(productDto);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                NotifyError("Informed ID is different of suppliers");
                return CustomResponse();
            }

            var product = await _productRepository.GetById(id);
            productDto.Image = product.Image;

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if(productDto.ImageUpload != null)
            {
                var imageName = Guid.NewGuid() + "-" + productDto.Image;

                productDto.Image = imageName;

                if (!UploadImage(productDto.ImageUpload, imageName))
                {
                    return CustomResponse();
                }
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.IsActive = productDto.IsActive;

            await _productService.Update(_mapper.Map<Product>(productDto));

            return CustomResponse(productDto);
        }

        private bool UploadImage(string file, string name)
        {
            if(string.IsNullOrEmpty(file))
            {
                NotifyError("File is empty");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(file);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", name);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("File already exists");
                return false;
            }

            //using (var stream = new FileStream(filePath, FileMode.Create)
            //{
                // CopyToAsync
            //}

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
            return true;
        }
    }
}
