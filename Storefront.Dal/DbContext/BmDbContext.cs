using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Storefront.Dal.Entities;
using System;

namespace Storefront.Dal.DbContext
{
  public class BmDbContext : IdentityDbContext<StorefrontUser, StorefrontRole, Guid>
  {
	public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }

	public BmDbContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	  optionsBuilder.UseSqlServer("server=.;database=Storefront.dev;Integrated Security=True;Persist Security Info=True");
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
	  builder.Entity<Category>(entity =>
	  {
		// Customize Entity Definition
	  });

	  base.OnModelCreating(builder);
	  // Customize the ASP.NET Identity model and override the defaults if needed. For example, you
	  // can rename the ASP.NET Identity table names and more. Add your customizations after calling base.OnModelCreating(builder);
	}
  }
}
