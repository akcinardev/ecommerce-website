namespace EcommerceApi.Dtos.Comment
{
	public class CommentDto
	{
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public int Rating { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
	}
}
