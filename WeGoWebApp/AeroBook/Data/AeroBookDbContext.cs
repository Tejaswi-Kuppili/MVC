using AeroBook.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace AeroBook.Data
{
	public class AeroBookDbContext: DbContext
	{
		public AeroBookDbContext(DbContextOptions options) : base(options) { }
		public DbSet<User> Users { get; set; }
	}
}
