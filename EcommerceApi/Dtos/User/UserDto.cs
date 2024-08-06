using EcommerceApi.Models;

namespace EcommerceApi.Dtos.User
{
    public class UserDto
    {
        public AppUser AppUser { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
