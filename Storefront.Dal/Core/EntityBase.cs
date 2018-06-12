using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storefront.Dal.Core
{
	public abstract class EntityBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public Guid CreatedById { get; set; }
		public Guid ModifiedById { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset ModifiedDate { get; set; }
		public bool IsNewRecord => this.Id == Guid.Empty;
	}
}
