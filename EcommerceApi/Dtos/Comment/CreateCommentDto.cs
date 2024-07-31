namespace EcommerceApi.Dtos.Comment
{
	public class CreateCommentDto
	{
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public int Rating { get; set; }
		public int? ProductId { get; set; }
	}
}
