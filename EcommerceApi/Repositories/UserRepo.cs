using EcommerceApi.Data;
using EcommerceApi.Dtos.User;
using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserRepo(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var userModel = new UserDto
            {
                AppUser = user,
                UserRoles = userRoles
            };

            return userModel;
        }

        public async Task<AppUser?> DeleteAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return null;
            }

            await _userManager.DeleteAsync(user);

            return user;
        }
    }
}
