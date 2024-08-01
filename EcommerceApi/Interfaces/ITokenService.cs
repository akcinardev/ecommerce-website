using EcommerceApi.Models;

namespace EcommerceApi.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(AppUser appUser);
	}
}
