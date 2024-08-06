using EcommerceApi.Dtos.Product;
using EcommerceApi.Dtos.User;
using EcommerceApi.Models;

namespace EcommerceApi.Interfaces
{
    public interface IUserRepo
    {
        Task<List<AppUser>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(string id);
        Task<AppUser?> DeleteAsync(string id);
    }
}
