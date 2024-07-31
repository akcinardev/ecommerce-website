using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApi.Models
{
	[Table("Comments")]
	public class Comment
	{
        public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
