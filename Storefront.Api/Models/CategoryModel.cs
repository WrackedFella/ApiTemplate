using Storefront.Api.Core;
using System.Collections.Generic;

namespace Storefront.Api.Models
{
  public class CategoryModel : ModelBase
  {
	public string Name { get; set; }

	public List<ProductModel> Products { get; set; }
  }
}
