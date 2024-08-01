using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Dtos.Comment
{
	public class UpdateCommentDto
	{

		[Required(ErrorMessage = "Title is required.")]
		[MinLength(5, ErrorMessage = "Title must be at least 5 characters.")]
		[MaxLength(50, ErrorMessage = "Title can not be over 50 characters.")]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "Content is required.")]
		[MinLength(10, ErrorMessage = "Content must be at least 10 characters.")]
		[MaxLength(300, ErrorMessage = "Content can not be over 300 characters.")]
		public string Content { get; set; } = string.Empty;

		[Required(ErrorMessage = "Rating is required.")]
		[Range(1, 5, ErrorMessage = "Rating must be in range of 1-5.")]
		public int Rating { get; set; }
	}
}
