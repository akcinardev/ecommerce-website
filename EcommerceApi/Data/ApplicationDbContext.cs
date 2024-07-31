using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
