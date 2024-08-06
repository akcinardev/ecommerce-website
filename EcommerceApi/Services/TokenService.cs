using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceApi.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;
		private readonly SymmetricSecurityKey _key;
		private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
			_userManager = userManager;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
        }

        public async Task<string> CreateToken(AppUser appUser)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Email, appUser.Email!),
				new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName!)
			};

            var userRoles = await _userManager.GetRolesAsync(appUser);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddHours(1),
				SigningCredentials = creds,
				Issuer = _config["JWT:Issuer"],
				Audience = _config["JWT:Audience"],
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
