using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models
{
	[Table("Sellers")]
	public class Seller
	{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public List<Product>? Products { get; set; } = new List<Product>();
    }
}
