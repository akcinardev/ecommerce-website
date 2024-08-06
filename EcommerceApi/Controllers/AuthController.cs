using EcommerceApi.Dtos.Account;
using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
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

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());

			if (user == null)
			{
				return Unauthorized("Invalid credentials!");
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (!result.Succeeded)
			{
				return Unauthorized("Invalid credentials!");
			}

			return Ok(new NewUserDto
			{
				Username = user.UserName,
				Email = user.Email,
				Token = await _tokenService.CreateToken(user)
			});
		}
	}
}
