using System;
using System.ComponentModel;
using Storefront.Api.Core;

namespace Storefront.Api.Models
{
	public class ProductModel : ModelBase
	{
		[DisplayName("Product")]
		public string Name { get; set; }

		[DisplayName("Category")]
		public string CategoryName => this.Category?.Name;

		[DisplayName("Price")]
		public decimal Price { get; set; }

		public Guid CategoryId { get; set; }

		public CategoryModel Category { get; set; }
	}
}
