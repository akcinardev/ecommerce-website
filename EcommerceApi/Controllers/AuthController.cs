using EcommerceApi.Dtos.Account;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterAccountDto registerDto)
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
					return Ok("User created successfully.");
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
	}
}
