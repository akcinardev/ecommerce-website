using EcommerceApi.Data;
using EcommerceApi.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Mappers;
using EcommerceApi.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var products = await _productRepo.GetAllAsync();
			
			var productsDto = products.Select(p => p.ToProductDto()).ToList();

            return Ok(productsDto);
        }

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var product = await _productRepo.GetByIdAsync(id);

			if (product == null)
			{
				return BadRequest($"No product found with specified ID:{id}");
			}

			var productDto = product.ToProductDto();

			return Ok(productDto);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var productModel = productDto.FromCreateDtoToProduct();
			await _productRepo.CreateAsync(productModel);

			return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel);
		}

		[HttpPut("{id:int}")]
		[Authorize]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto productDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var updatedProduct = await _productRepo.UpdateAsync(id, productDto);

			return Ok(updatedProduct);
		}

		[HttpDelete("{id:int}")]
		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var product = await _productRepo.DeleteAsync(id);

			if (product == null)
			{
				return BadRequest($"No product found with specified ID:{id}");
			}

			return NoContent();
		}
	}
}
