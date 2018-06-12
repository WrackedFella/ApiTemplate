using Microsoft.AspNetCore.JsonPatch;
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
	public class ProductControllerTests : TestBed
	{
		protected StorefrontDbContext BuildContext(params Product[] products)
		{
			var context = base.BuildContext();
			context.Products.AddRange(products);
			context.SaveChanges();
			return context;
		}

		public ProductControllerTests(AutoMapperFixture fixture) : base(fixture)
		{
		}

		[Fact]
		public async Task Post_GivenNewProduct_ShouldInsertProduct()
		{
			// Arrange
			var context = this.BuildContext();
			var controller = new ProductController(context, null);
			var model = new ProductModel();

			// Act
			var result = (await controller.Post(model)).Value;
			var returnValue = result.RouteValues.Values.First().ToString();
			var product = context.Products.FirstOrDefault(x => x.Id == Guid.Parse(returnValue));

			// Assert
			Assert.Equal(201, result.StatusCode);
			Assert.NotNull(product);
		}

		[Fact]
		public async Task Patch_GivenNewName_ShouldUpdateNameButNotPrice()
		{
			// Arrange
			var expectedPrice = 1.00m;
			var expectedName = "Green Beans";
			var product = new Product { Name = "Canned Corn", Price = expectedPrice };
			var context = this.BuildContext(product);
			var controller = new ProductController(context, null);
			var patch = new JsonPatchDocument<ProductModel>();
			patch.Replace(x => x.Name, expectedName);

			// Act
			var result = (await controller.Patch(product.Id, patch)).Value;
			var returnValue = result.RouteValues.Values.First().ToString();
			product = context.Products.FirstOrDefault(x => x.Id == Guid.Parse(returnValue));

			// Assert
			Assert.Equal(202, result.StatusCode);
			Assert.NotNull(product);
			Assert.Equal(expectedName, product.Name);
			Assert.Equal(expectedPrice, product.Price);
		}

		[Fact]
		public async Task Put_GivenNewName_ShouldUpdateNameAndBlankPrice()
		{
			// Arrange
			var expectedName = "Green Beans";
			var product = new Product { Name = "Canned Corn", Price = 1.00m };
			var context = this.BuildContext(product);
			var controller = new ProductController(context, null);
			var model = new ProductModel { Name = expectedName };

			// Act
			var result = (await controller.Put(product.Id, model)).Value;
			var returnValue = result.RouteValues.Values.First().ToString();
			product = context.Products.FirstOrDefault(x => x.Id == Guid.Parse(returnValue));

			// Assert
			Assert.Equal(202, result.StatusCode);
			Assert.NotNull(product);
			Assert.Equal(expectedName, product.Name);
			Assert.Equal(0m, product.Price);
		}
	}
}
