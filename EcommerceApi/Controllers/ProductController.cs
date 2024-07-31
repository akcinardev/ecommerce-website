using EcommerceApi.Data;
using EcommerceApi.Dtos.Product;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Mappers;

namespace EcommerceApi.Controllers
{
    [Route("api/products")]
    [ApiController]
	public class ProductController : ControllerBase
	{
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var product = _context.Products.Find(id);
			return Ok(product);
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateProductDto productDto)
		{
			var product = productDto.FromCreateDtoToProduct();

			_context.Products.Add(product);
			_context.SaveChanges();

			return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromRoute] int id, [FromBody] UpdateProductDto productDto)
		{
			var existingProduct = _context.Products.Find(id);
			var updatedProduct = productDto.FromUpdateDtoToProduct();

			existingProduct.Name = updatedProduct.Name;
			existingProduct.Description = updatedProduct.Description;
			existingProduct.Price = updatedProduct.Price;
			existingProduct.Currency = updatedProduct.Currency;
			existingProduct.Category = updatedProduct.Category;
			existingProduct.StockAmount = updatedProduct.StockAmount;

			_context.SaveChanges();

			return Ok(existingProduct);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var product = _context.Products.Find(id);
			_context.Products.Remove(product);
			_context.SaveChanges();
			return Ok();
		}
	}
}
