using EcommerceApi.Data;
using EcommerceApi.Dtos.Product;
using EcommerceApi.Interfaces;
using EcommerceApi.Mappers;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
	public class ProductRepo : IProductRepo
	{
		private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }

		public async Task<List<Product>> GetAllAsync()
		{
			var products = await _context.Products
				.Include(p => p.Seller)
				.Include(p => p.Comments)
				.ToListAsync();
			return products;
		}

		public async Task<Product?> GetByIdAsync(int id)
		{
			var product = await _context.Products
				.Include(p => p.Seller)
				.Include(p => p.Comments)
				.SingleOrDefaultAsync(p => p.Id == id);
			if (product == null)
			{
				return null;
			}
			return product;
		}

		public async Task<Product> CreateAsync(Product productModel)
		{
			await _context.Products.AddAsync(productModel);
			await _context.SaveChangesAsync();

			return productModel;
		}

		public async Task<Product?> DeleteAsync(int id)
		{
			var product = await _context.Products.FindAsync(id);

			if (product == null)
			{
				return null;
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return product;
		}

		public async Task<Product?> UpdateAsync(int id, UpdateProductDto productDto)
		{
			var existingProduct = await _context.Products.FindAsync(id);

			if (existingProduct == null)
			{
				return null;
			}

			var updatedProduct = productDto.FromUpdateDtoToProduct();

			existingProduct.Name = updatedProduct.Name;
			existingProduct.Description = updatedProduct.Description;
			existingProduct.Price = updatedProduct.Price;
			existingProduct.Currency = updatedProduct.Currency;
			existingProduct.Category = updatedProduct.Category;
			existingProduct.StockAmount = updatedProduct.StockAmount;

			await _context.SaveChangesAsync();

			return existingProduct;
		}
	}
}
