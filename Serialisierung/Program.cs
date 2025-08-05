using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		string folderPath = "Test";

		string filePath = Path.Combine(folderPath, "Fahrzeuge.xml");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge =
		[
			new PKW(251, FahrzeugMarke.BMW),
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

		//XML

		//1. XML lesen/schreiben
		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());

		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		{
			xml.Serialize(fs, fahrzeuge);
		}

		using (FileStream fs = new FileStream(filePath, FileMode.Open))
		{
			List<Fahrzeug> fzg = (List<Fahrzeug>) xml.Deserialize(fs);
		}

		//2. Attribute
		//XmlIgnore
		//XmlAttribute: Feld wird als Attribut geschrieben (im XML-Tag Name=Wert)
		//XmlInclude: Vererbung aktivieren (identisch zu JsonDerivedType)
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		{
			xml.Serialize(fs, fahrzeuge);
		}

		//3. XML per Hand
		XmlDocument doc = new();
		doc.Load(filePath);
		foreach (XmlNode node in doc.DocumentElement)
		{
			XmlNode typeNode = node.Attributes.GetNamedItem("xsi:type");
			if (typeNode != null)
				Console.WriteLine(typeNode.Value);

			Console.WriteLine(node.Attributes["MaxV"].Value);
			Console.WriteLine(node.Attributes["Marke"].Value);
		}
	}

	static void SystemJson()
	{
		//string folderPath = "Test";

		//string filePath = Path.Combine(folderPath, "Fahrzeuge.json");

		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//List<Fahrzeug> fahrzeuge =
		//[
		//	new PKW(251, FahrzeugMarke.BMW),
		//	new Fahrzeug(274, FahrzeugMarke.BMW),
		//	new Fahrzeug(146, FahrzeugMarke.BMW),
		//	new Fahrzeug(208, FahrzeugMarke.Audi),
		//	new Fahrzeug(189, FahrzeugMarke.Audi),
		//	new Fahrzeug(133, FahrzeugMarke.VW),
		//	new Fahrzeug(253, FahrzeugMarke.VW),
		//	new Fahrzeug(304, FahrzeugMarke.BMW),
		//	new Fahrzeug(151, FahrzeugMarke.VW),
		//	new Fahrzeug(250, FahrzeugMarke.VW),
		//	new Fahrzeug(217, FahrzeugMarke.Audi),
		//	new Fahrzeug(125, FahrzeugMarke.Audi)
		//];

		////System.Text.Json

		////1. Json lesen/schreiben
		//string json = JsonSerializer.Serialize(fahrzeuge);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson);

		////2. Json Settings/Options
		//JsonSerializerOptions options = new();
		//options.IncludeFields = true; //WICHTIG: Bei Json/XML müssen alle Felder Properties sein
		//options.WriteIndented = true;

		//string json2 = JsonSerializer.Serialize(fahrzeuge, options); //Options müssen hier mitgegeben werden
		//File.WriteAllText(filePath, json2);

		////3. Attribute
		////JsonIgnore: Ignoriert das gegebene Feld
		////JsonPropertyName/JsonPropertyOrder: Feld umbenennen, Felder anders ordnen
		////JsonExtensionData: Fängt Felder auf, welche in der Klasse keine entsprechenden Felder haben
		////JsonDerivedType: Vererbung aktivieren (muss auf der obersten Klasse alle Unterklassen registrieren)

		//string json3 = JsonSerializer.Serialize(fahrzeuge, options); //Options müssen hier mitgegeben werden
		//File.WriteAllText(filePath, json3);

		//string readJson2 = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg2 = JsonSerializer.Deserialize<Fahrzeug[]>(readJson2, options);

		////4. Json per Hand
		////Vorallem bei unbekanntem Json (aus dem Internet)
		//JsonDocument doc = JsonDocument.Parse(json3);
		//foreach (JsonElement element in doc.RootElement.EnumerateArray())
		//{
		//	string type = element.GetProperty("$type").GetString();
		//	int maxV = element.GetProperty("MaxV").GetInt32();
		//	FahrzeugMarke marke = (FahrzeugMarke) element.GetProperty("Marke").GetInt32();

		//	Console.WriteLine($"{type}, {maxV}, {marke}");
		//}
	}

	static void NewtonsoftJson()
	{
		string folderPath = "Test";

		string filePath = Path.Combine(folderPath, "Fahrzeuge.json");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge =
		[
			new PKW(251, FahrzeugMarke.BMW),
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

		//Newtonsoft.Json

		//1. Json lesen/schreiben
		string json = JsonConvert.SerializeObject(fahrzeuge);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson);

		//2. Json Settings/Options
		JsonSerializerSettings settings = new();
		//settings.Formatting = Formatting.Indented;
		settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung

		string json2 = JsonConvert.SerializeObject(fahrzeuge, settings);
		File.WriteAllText(filePath, json2);

		//3. Attribute
		//JsonIgnore: Ignoriert das gegebene Feld
		//JsonProperty: Feld anpassen
		//JsonExtensionData: Fängt Felder auf, welche in der Klasse keine entsprechenden Felder haben

		//4. Json per Hand
		//Vorallem bei unbekanntem Json (aus dem Internet)
		JToken doc = JToken.Parse(json2);
		foreach (JToken token in doc)
		{
			string type = token["$type"].Value<string>();
			int maxV = token["MaxV"].Value<int>();
			FahrzeugMarke marke = (FahrzeugMarke) token["Marke"].Value<int>();

			Console.WriteLine($"{type}, {maxV}, {marke}");
		}
	}
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]
//[JsonDerivedType(typeof(Fahrzeug), "Fzg")]
//[JsonDerivedType(typeof(PKW), "PKW")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
public class Fahrzeug(int maxV, FahrzeugMarke marke)
{
	//[JsonIgnore]
	[XmlAttribute]
	public int MaxV { get; set; } = maxV;

	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; } = marke;

	//[JsonExtensionData]
	//public Dictionary<string, object> ExtraData { get; set; } = [];

	public Fahrzeug() : this(0, FahrzeugMarke.Audi)
	{
		
	}
}

public class PKW : Fahrzeug
{
	public PKW(int maxV, FahrzeugMarke marke) : base(maxV, marke) { }

	public PKW()
	{
		
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }