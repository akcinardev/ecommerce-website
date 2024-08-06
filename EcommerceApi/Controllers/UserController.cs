using EcommerceApi.Data;
using EcommerceApi.Dtos.Account;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Policy = "RequireOwnerRole")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _context.Users.FindAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);

            var userDto = new
            {
                User = user,
                Roles = userRoles
            };

            return Ok(userDto);
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
            var existingUser = await _context.Users.FindAsync(id);

            try
            {
                await _userManager.DeleteAsync(existingUser);
                return Ok(existingUser);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
