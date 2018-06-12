using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storefront.Dal.Core
{
	public abstract class EntityBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string CreatedByUsername { get; set; }
		public string ModifiedByUsername { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset ModifiedDate { get; set; }
		public bool IsNewRecord => this.Id == Guid.Empty;
	}
}
