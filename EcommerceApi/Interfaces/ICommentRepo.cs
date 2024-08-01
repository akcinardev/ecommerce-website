using EcommerceApi.Dtos.Comment;
using EcommerceApi.Models;

namespace EcommerceApi.Interfaces
{
	public interface ICommentRepo
	{
		Task<List<Comment>> GetAllAsync();
		Task<Comment?> GetByIdAsync(int id);
		Task<Comment> CreateAsync(Comment commentModel);
		Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto);
		Task<Comment?> DeleteAsync(int id);
	}
}
