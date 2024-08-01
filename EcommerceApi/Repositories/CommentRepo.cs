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

        public async Task<Comment> CreateAsync(CreateCommentDto commentDto)
		{
			var comment = commentDto.FromCreateDtoToComment();

			await _context.Comments.AddAsync(comment);
			await _context.SaveChangesAsync();

			return comment;
		}

		public  Task<Comment?> DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Comment>> GetAllAsync()
		{
			var comments = await _context.Comments.ToListAsync();
			
			return comments;
		}

		public async Task<Comment?> GetByIdAsync(int id)
		{
			var comment = await _context.Comments.SingleOrDefaultAsync(c => c.Id == id);

			return comment;
		}

		public Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
		{
			throw new NotImplementedException();
		}
	}
}
