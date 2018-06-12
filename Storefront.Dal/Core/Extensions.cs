using System;
using System.Collections.Generic;
using System.Text;

namespace Storefront.Dal.Core
{
	public static class Extensions
	{
		public static void SetAuditDetails(this EntityBase entity, string username)
		{
			if (entity.IsNewRecord)
			{
				entity.CreatedByUsername = username;
				entity.CreatedDate = DateTimeOffset.Now;
			}
			entity.ModifiedByUsername = username;
			entity.ModifiedDate = DateTimeOffset.Now;
		}
	}
}
