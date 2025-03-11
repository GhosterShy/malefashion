using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace malefashion.Models
{
	public class AppIdentityDBContext : IdentityDbContext<AppUser>
	{
		public AppIdentityDBContext(DbContextOptions<AppIdentityDBContext> options)
			: base(options)
		{
		}
	}
}
