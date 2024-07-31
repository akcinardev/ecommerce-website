namespace EcommerceApi.Dtos.Comment
{
	public class UpdateCommentDto
	{
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public int Rating { get; set; }
	}
}
