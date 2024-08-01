using EcommerceApi.Models;

namespace EcommerceApi.Dtos.Product
{
	public class CreateProductDto
	{
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public double Price { get; set; }
		public string Currency { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public int StockAmount { get; set; }
	}
}
