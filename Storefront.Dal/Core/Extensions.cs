using System;
using System.Collections.Generic;
using System.Text;

namespace Storefront.Dal.Core
{
	public static class Extensions
	{
		public static void SetAuditDetails(this EntityBase entity, Guid userId)
		{
			if (entity.IsNewRecord)
			{
				entity.CreatedById = userId;
				entity.CreatedDate = DateTimeOffset.Now;
			}
			entity.ModifiedById = userId;
			entity.ModifiedDate = DateTimeOffset.Now;
		}
	}
}
