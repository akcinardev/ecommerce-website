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
        public string Category { get; set; } = string.Empty;
        public int StockAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
