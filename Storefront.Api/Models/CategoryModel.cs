using Storefront.Api.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace Storefront.Api.Models
{
	public class CategoryModel : ModelBase
	{
		[DisplayName("Category")]
		public string Name { get; set; }

		[DisplayName("Product List")]
		public List<ProductModel> Products { get; set; }
	}
}
