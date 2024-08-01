using EcommerceApi.Dtos.Product;
using EcommerceApi.Models;

namespace EcommerceApi.Interfaces
{
	public interface IProductRepo
	{
		Task<List<Product>> GetAllAsync();
		Task<Product?> GetByIdAsync(int id);
		Task<Product> CreateAsync(Product productModel);
		Task<Product?> UpdateAsync(int id, UpdateProductDto productDto);
		Task<Product?> DeleteAsync(int id);
	}
}
