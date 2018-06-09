using Microsoft.Extensions.Logging;
using Storefront.Api.Core;
using Storefront.Api.Models;
using Storefront.Dal.DbContext;
using Storefront.Dal.Entities;

namespace Storefront.Api.ApiControllers
{
  public class CategoryController : ControllerBase<Category, CategoryModel>
  {
	public CategoryController(BmDbContext context, ILogger logger) : base(context, logger)
	{
	}
  }
}
