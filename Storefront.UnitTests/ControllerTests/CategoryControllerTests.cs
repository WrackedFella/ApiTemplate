using Storefront.Api.ApiControllers;
using Storefront.Api.Models;
using Storefront.Dal.DbContext;
using Storefront.Dal.Entities;
using Storefront.UnitTests.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Storefront.UnitTests.ControllerTests
{
	public class CategoryControllerTests : TestBed
	{
		protected StorefrontDbContext BuildContext(params Category[] products)
		{
			var context = base.BuildContext();
			context.Categories.AddRange(products);
			context.SaveChanges();
			return context;
		}

		public CategoryControllerTests(AutoMapperFixture fixture) : base(fixture)
		{
		}

		[Fact]
		public async Task Create_GivenNewCategory_ShouldInsertCategory()
		{
			// Arrange
			var context = this.BuildContext();
			var controller = new CategoryController(context);
			var model = new CategoryModel();

			// Act
			var result = (await controller.Post(model)).Value;
			var returnValue = result.RouteValues.Values.First().ToString();
			var category = context.Categories.FirstOrDefault(x => x.Id == Guid.Parse(returnValue));

			// Assert
			Assert.NotNull(result);
			Assert.Equal(201, result.StatusCode);
			Assert.NotNull(category);
		}
	}
}
