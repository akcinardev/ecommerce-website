using EcommerceApi.Dtos.Comment;

namespace EcommerceApi.Dtos.Product
{
	public class ProductDto
	{
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public double Price { get; set; }
		public string Currency { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public int? Rating { get; set; }
		public int StockAmount { get; set; }
		public List<CommentDto>? Comments { get; set; } = new List<CommentDto>();
	}
}
