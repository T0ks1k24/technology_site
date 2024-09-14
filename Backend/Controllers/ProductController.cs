using Backend.Models.DTOs.Products;
using Backend.Models.Entity;
using Backend.Repositories.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepositories)
        {
            _productRepository = productRepositories;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productDomain = await _productRepository.GetAllAsync();

            var productDtos = productDomain.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
            });

            return Ok(productDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var productDomain = await _productRepository.GetByIdAsync(id);

            if (productDomain == null)
            {
                return NotFound();
            }

            var productDtos = new ProductDto
            {
                Id = productDomain.Id,
                Name = productDomain.Name,
                Description = productDomain.Description,
                Price = productDomain.Price,
                ImageUrl = productDomain.ImageUrl,
            };

            return Ok(productDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUpdateProductDto addUpdateProductDto)
        {
            var productDomain = new ProductEntity
            {
                Name = addUpdateProductDto.Name,
                Description = addUpdateProductDto.Description,
                Price = addUpdateProductDto.Price,
                ImageUrl = addUpdateProductDto.ImageUrl,
            };

            await _productRepository.CreateAsync(productDomain);

            var productDto = new ProductDto
            {
                Id = productDomain.Id,
                Name = productDomain.Name,
                Description = productDomain.Description,
                Price = productDomain.Price,
                ImageUrl = productDomain.ImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new {id = productDto.Id}, productDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AddUpdateProductDto addUpdateProductDto)
        {
            var productDomain = new ProductEntity
            {
                Name = addUpdateProductDto.Name,
                Description = addUpdateProductDto.Description,
                Price = addUpdateProductDto.Price,
                ImageUrl = addUpdateProductDto.ImageUrl,
            };


            productDomain = await _productRepository.UpdateAsync(id, productDomain);

            if (productDomain == null)
            {
                return NotFound();
            }

            var productDto = new ProductDto
            {
                Id = productDomain.Id,
                Name = productDomain.Name,
                Description = productDomain.Description,
                Price = productDomain.Price,
                ImageUrl = productDomain.ImageUrl,
            };

            return Ok(productDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteProduct = await _productRepository.DeleteAsync(id);

            var productDto = new ProductDto
            {
                Id = deleteProduct.Id,
                Name = deleteProduct.Name,
                Description = deleteProduct.Description,
                Price = deleteProduct.Price,
                ImageUrl = deleteProduct.ImageUrl,
            };

            return Ok(productDto);
        }

    }
}

