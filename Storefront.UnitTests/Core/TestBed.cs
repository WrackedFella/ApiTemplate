using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Storefront.Dal.DbContext;
using Xunit;

namespace Storefront.UnitTests.Core
{
  [Collection("AutoMapper")]
  public class TestBed
  {
	protected AutoMapperFixture Fixture;

	protected TestBed(AutoMapperFixture fixture)
	{
	  this.Fixture = fixture;
	}

	protected virtual BmDbContext BuildContext()
	{
	  var serviceProvider = new ServiceCollection()
		  .AddEntityFrameworkInMemoryDatabase()
		  .BuildServiceProvider();

	  DbContextOptions<BmDbContext> options = new DbContextOptionsBuilder<BmDbContext>()
		  .UseInMemoryDatabase("MockStorefrontDb")
		  // Without this line, data will persist between tests. May be desirable, under certain conditions.
		  .UseInternalServiceProvider(serviceProvider)
		  .EnableSensitiveDataLogging()
		  .Options;

	  return new BmDbContext(options);
	}
  }
}
