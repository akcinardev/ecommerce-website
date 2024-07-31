using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models
{
	[Table("Products")]
	public class Product
	{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
		public double Price { get; set; }
        public string Currency { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public int StockAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Comment>? Comments { get; set; } = new List<Comment>();
        public int SellerId { get; set; }
		public Seller Seller { get; set; }
    }
}
