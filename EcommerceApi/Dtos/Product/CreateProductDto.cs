using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Dtos.Product
{
	public class CreateProductDto
	{
		[Required(ErrorMessage = "Name is required.")]
		[MinLength(5, ErrorMessage = "Name must be at least 5 characters.")]
		[MaxLength(50, ErrorMessage = "Name can not be over 50 characters.")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Description is required.")]
		[MinLength(50, ErrorMessage = "Description must be at least 50 characters.")]
		[MaxLength(500, ErrorMessage = "Description can not be over 500 characters.")]
		public string? Description { get; set; }

		[Required(ErrorMessage = "Price is required.")]
		[Range(1, 99999, ErrorMessage = "Price must be in range of 1 - 99.999")]
		public double Price { get; set; }

		[Required(ErrorMessage = "Currency is required.")]
		[MinLength(1, ErrorMessage = "Currency must be at least 1 characters.")]
		[MaxLength(6, ErrorMessage = "Currency can not be over 6 characters.")]
		public string Currency { get; set; } = string.Empty;

		[Required(ErrorMessage = "Category is required.")]
		[MaxLength(20, ErrorMessage = "Category can not be over 20 characters.")]
		public string Category { get; set; } = string.Empty;

		[Required(ErrorMessage = "Stock amount is required.")]
		[Range(1, 99999, ErrorMessage = "Stock amount must be in range of 1 - 99.999")]
		public int StockAmount { get; set; }
	}
}
