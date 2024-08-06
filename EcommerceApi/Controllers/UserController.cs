using EcommerceApi.Data;
using EcommerceApi.Dtos.Account;
using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Owner")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepo _userRepo;

        public UserController(ApplicationDbContext context, UserManager<AppUser> userManager, IUserRepo userRepo)
        {
            _context = context;
            _userManager = userManager;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (user == null)
            {
                return BadRequest("No user found with specified ID");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterAccountDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var user = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.EmailAddress
                };

                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    return Ok(user);
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingUser = await _userRepo.DeleteAsync(id);

            if (existingUser == null)
            {
                return BadRequest("No user found with specified ID");
            }

            return NoContent();
        }
    }
}
