using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Storefront.Dal.DbContext
{
	internal class StorefrontDbContextFactory : IDesignTimeDbContextFactory<StorefrontDbContext>
	{
		public StorefrontDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<StorefrontDbContext>();
			optionsBuilder.UseSqlServer("server=.;database=Storefront.dev;Integrated Security=True;Persist Security Info=True");

			return new StorefrontDbContext(optionsBuilder.Options);
		}
	}
}
