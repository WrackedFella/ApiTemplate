using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Storefront.Api.Core
{
	public class DisplayNameConverter : JsonConverter
	{
		public string DisplayNames { get; set; }

		private string DisplayNamesPostfix => string.IsNullOrEmpty(this.DisplayNames) ? string.Empty : " " + this.DisplayNames;

		public DisplayNameConverter(string units)
		{
			this.DisplayNames = units;
		}

		public override bool CanConvert(Type objectType)
		{
			throw new NotImplementedException(); // Not called when applied directly to a property.
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jvalue = JToken.Load(reader);
			if (jvalue.Type == JTokenType.String)
			{
				var s = (string)jvalue;
				if (s.EndsWith(this.DisplayNames))
					jvalue = (JValue)s.Substring(0, s.LastIndexOf(this.DisplayNames, StringComparison.Ordinal)).Trim();
			}
			return jvalue.ToObject(objectType);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var jvalue = JToken.FromObject(value);
			if (jvalue.Type == JTokenType.Null)
				jvalue.WriteTo(writer);
			else
				writer.WriteValue(jvalue.ToString() + this.DisplayNamesPostfix);
		}
	}
}
