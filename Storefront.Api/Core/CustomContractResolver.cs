using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Storefront.Api.Core
{
	public class CustomContractResolver : CamelCasePropertyNamesContractResolver
	{
		public CustomContractResolver()
		{
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			if (property.Converter == null && property.MemberConverter == null)
			{
				var attr = property.AttributeProvider.GetAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().FirstOrDefault(a => !string.IsNullOrEmpty(a.DisplayName));
				if (attr != null)
				{
					property.Converter = property.MemberConverter = new DisplayNameConverter(attr.DisplayName);
				}
			}
			return property;
		}
	}
}
