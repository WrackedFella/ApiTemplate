using Storefront.Api.Core;

namespace Storefront.Api.Models
{
  public class ProductModel : ModelBase
  {
	public string Name { get; set; }
	public string CategoryName => this.Category.Name;
	public decimal Price { get; set; }

	public CategoryModel Category { get; set; }
  }
}
