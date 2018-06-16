using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Storefront.Api.Core
{
	public class CustomJsonConverter : JsonConverter
	{
		private readonly Type[] _types;

		public CustomJsonConverter(params Type[] types)
		{
			this._types = types;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var t = JToken.FromObject(value);

			if (t.Type != JTokenType.Object)
			{
				t.WriteTo(writer);
			}
			else
			{
				var o = (JObject)t;
				IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

				o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

				o.WriteTo(writer);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
		}

		public override bool CanRead => false;

		public override bool CanConvert(Type objectType)
		{
			return this._types.Any(t => t == objectType);
		}
	}
}
