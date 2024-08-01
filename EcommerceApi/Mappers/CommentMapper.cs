using EcommerceApi.Dtos.Comment;
using EcommerceApi.Models;

namespace EcommerceApi.Mappers
{
	public static class CommentMapper
	{
		public static CommentDto ToCommentDto(this Comment comment)
		{
			return new CommentDto
			{
				ProductName = comment.Product.Name,
				Title = comment.Title,
				Content = comment.Content,
				Rating = comment.Rating,
				CreatedDate = comment.CreatedDate
			};
		}

		public static Comment FromCreateDtoToComment(this CreateCommentDto commentDto)
		{
			return new Comment
			{
				Title = commentDto.Title,
				Content = commentDto.Content,
				Rating = commentDto.Rating,
				ProductId = commentDto.ProductId
			};
		}

		public static Comment FromUpdateDtoToComment(this UpdateCommentDto commentDto)
		{
			return new Comment
			{
				Title = commentDto.Title,
				Content = commentDto.Content,
				Rating = commentDto.Rating,
			};
		}
	}
}
