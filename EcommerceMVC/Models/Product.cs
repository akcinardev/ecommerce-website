using System.Xml.Linq;

namespace EcommerceMVC.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public int StockAmount { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
