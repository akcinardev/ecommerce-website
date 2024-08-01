using EcommerceApi.Data;
using EcommerceApi.Dtos.Comment;
using EcommerceApi.Interfaces;
using EcommerceApi.Mappers;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
	public class CommentRepo : ICommentRepo
	{
		private readonly ApplicationDbContext _context;

        public CommentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
		{
			await _context.Comments.AddAsync(commentModel);
			await _context.SaveChangesAsync();

			return commentModel;
		}

		public  async Task<Comment?> DeleteAsync(int id)
		{
			var comment = await _context.Comments.FindAsync(id);

			if (comment == null)
			{
				return null;
			}

			_context.Remove(comment);
			await _context.SaveChangesAsync();

			return comment;
		}

		public async Task<List<Comment>> GetAllAsync()
		{
			var comments = await _context.Comments.Include(c => c.Product).ToListAsync();
			
			return comments;
		}

		public async Task<Comment?> GetByIdAsync(int id)
		{
			var comment = await _context.Comments.Include(c => c.Product).SingleOrDefaultAsync(c => c.Id == id);

			return comment;
		}

		public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
		{
			var existingComment = await _context.Comments.FindAsync(id);

			if (existingComment == null)
			{
				return null;
			}

			var updatedComment = commentDto.FromUpdateDtoToComment();

			existingComment.Title = updatedComment.Title;
			existingComment.Content = updatedComment.Content;
			existingComment.Rating = updatedComment.Rating;

			await _context.SaveChangesAsync();

			return existingComment;
		}
	}
}
