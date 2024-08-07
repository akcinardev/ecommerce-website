﻿using EcommerceApi.Data;
using EcommerceApi.Dtos.Comment;
using EcommerceApi.Interfaces;
using EcommerceApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
	[Route("api/comment")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly ICommentRepo _commentRepo;

        public CommentController(ApplicationDbContext context, ICommentRepo commentRepo)
        {
            _context = context;
			_commentRepo = commentRepo;
        }

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var comments = await _commentRepo.GetAllAsync();

			var commentDto = comments.Select(c => c.ToCommentDto()).ToList();

			return Ok(commentDto);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var comment = await _commentRepo.GetByIdAsync(id);

			if (comment == null)
			{
				return BadRequest($"No comment found with specified ID:{id}");
			}

			return Ok(comment.ToCommentDto());
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create([FromBody] CreateCommentDto commentDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var commentModel = commentDto.FromCreateDtoToComment();

			await _commentRepo.CreateAsync(commentModel);

			return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel);
		}

		[HttpPut("{id:int}")]
		[Authorize]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto commentDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var updatedComment = await _commentRepo.UpdateAsync(id, commentDto);

			if (updatedComment == null)
			{
				return BadRequest($"No comment found with specified ID: {id}");
			}

			return Ok(updatedComment);
		}

		[HttpDelete("{id:int}")]
		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var deletedComment = await _commentRepo.DeleteAsync(id);
			
			if (deletedComment == null)
			{
				return BadRequest($"No comment found with specified ID: {id}");
			}

			return NoContent();
		}
	}
}
