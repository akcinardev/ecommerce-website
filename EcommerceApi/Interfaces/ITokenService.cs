using EcommerceApi.Models;

namespace EcommerceApi.Interfaces
{
	public interface ITokenService
	{
		Task<string> CreateToken(AppUser appUser);
	}
}
