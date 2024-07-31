using EcommerceApi.Data;
using EcommerceApi.Dtos.Product;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Mappers;
using EcommerceApi.Repositories;
using EcommerceApi.Interfaces;

namespace EcommerceApi.Controllers
{
    [Route("api/products")]
    [ApiController]
	public class ProductController : ControllerBase
	{
        private readonly ApplicationDbContext _context;
        private readonly IProductRepo _productRepo;

		public ProductController(ApplicationDbContext context, IProductRepo productRepo)
        {
            _context = context;
			_productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepo.GetAllAsync();
            return Ok(products);
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await _productRepo.GetByIdAsync(id);

			if (product == null)
			{
				return BadRequest($"No product found with specified ID:{id}");
			}

			return Ok(product);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
		{
			var product = await _productRepo.CreateAsync(productDto);

			return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto productDto)
		{
			var updatedProduct = await _productRepo.UpdateAsync(id, productDto);

			return Ok(updatedProduct);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productRepo.DeleteAsync(id);

			if (product == null)
			{
				return BadRequest($"No product found with specified ID:{id}");
			}

			return NoContent();
		}
	}
}
