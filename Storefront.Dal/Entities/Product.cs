using Storefront.Dal.Core;
using System;

namespace Storefront.Dal.Entities
{
  public class Product : EntityBase
  {
	public string Name { get; set; }
	public decimal Price { get; set; }
	public Guid CategoryId { get; set; }

	public virtual Category Category { get; set; }
  }
}
