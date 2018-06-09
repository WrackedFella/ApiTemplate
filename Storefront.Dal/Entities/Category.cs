using Storefront.Dal.Core;
using System.Collections.Generic;

namespace Storefront.Dal.Entities
{
  public class Category : EntityBase
  {
	public string Name { get; set; }

	public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
  }
}
