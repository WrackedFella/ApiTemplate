using Microsoft.Extensions.Logging;
using Storefront.Api.Core;
using Storefront.Api.Models;
using Storefront.Dal.DbContext;
using Storefront.Dal.Entities;

namespace Storefront.Api.ApiControllers
{
  public class ProductController : ControllerBase<Product, ProductModel>
  {
	public ProductController(BmDbContext context, ILogger logger) : base(context, logger)
	{
	}
  }
}
