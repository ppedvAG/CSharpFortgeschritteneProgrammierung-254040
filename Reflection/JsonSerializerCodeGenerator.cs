using System.Diagnostics;
using System.Text;
using System.Text.Json;
using static System.Text.Json.JsonElement;

namespace Reflection;

public class JsonSerializerCodeGenerator
{
	static void Main(string[] args)
	{
		Type je = typeof(JsonElement);
		IEnumerable<string> select = je.GetMethods().Where(e => !e.Name.Contains("Try")).Select(e => $"{e.ReturnType.Name} => element.{e.Name}()");
		StringBuilder sb = new("return element switch\n{\n");
		foreach (string selectItem in select)
		{
			sb.Append('\t');
			sb.Append(selectItem);
			sb.Append(",\n");
		}
		sb.Append("\t_ => throw new Exception(\"Unbekannter Typ\")\n};");
		string switchPattern = sb.ToString();

		List<Fahrzeug> fahrzeuge =
		[
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		];

		string json = JsonSerializer.Serialize(fahrzeuge);

		JsonDocument doc = JsonDocument.Parse(json);

		foreach (JsonElement element in doc.RootElement.EnumerateArray())
		{
			element.GetProperty<int>("MaxV");
		}
	}
}


[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]
public class Fahrzeug(int maxV, FahrzeugMarke marke)
{
	public int MaxV { get; set; } = maxV;

	public FahrzeugMarke Marke { get; set; } = marke;
}

public enum FahrzeugMarke { Audi, BMW, VW }

public static class JsonExtensions
{
	public static T GetProperty<T>(this JsonElement e, string propName)
	{
		JsonElement element = e.GetProperty(propName);
		return default(T) switch
		{
			bool => element.GetBoolean(),
			string => element.GetString(),
			byte[] => element.GetBytesFromBase64(),
			sbyte => element.GetSByte(),
			byte => element.GetByte(),
			short => element.GetInt16(),
			ushort => element.GetUInt16(),
			int => element.GetInt32(),
			uint => element.GetUInt32(),
			long => element.GetInt64(),
			ulong => element.GetUInt64(),
			double => element.GetDouble(),
			float => element.GetSingle(),
			decimal => element.GetDecimal(),
			DateTime => element.GetDateTime(),
			DateTimeOffset => element.GetDateTimeOffset(),
			Guid => element.GetGuid(),
			_ => throw new Exception("Unbekannter Typ")
		};
	}
}