using Goods.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Goods.Backend.Data
{
	public class GoodContext : DbContext
	{
		public GoodContext(DbContextOptions<GoodContext> options)
			: base(options)
		{ }

		public DbSet<Good> Goods { get; set; }
	}
}
