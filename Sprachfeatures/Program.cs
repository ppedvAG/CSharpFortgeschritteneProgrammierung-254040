using System.Collections;
using System.Security.Cryptography.X509Certificates;
using IntList = System.Collections.Generic.List<int>;

namespace Sprachfeatures;

internal unsafe class Program
{
	public readonly int Zahl = 1;

	public Program()
	{
		Zahl = 1;
	}

	static unsafe void Main(string[] args)
	{
		object o = null;
		if (o is int x)
		{
			//Vererbungshierarchietypvergleich
			//Muss bei Interfaces verwendet werden

			//int x = (int) o;
			Console.WriteLine(x * 2);
		}

		if (o.GetType() == typeof(int))
		{
			//Genauer Typvergleich
		}

		double d = 2_482_937_591_385.12_358_237_958;

		//class und struct

		//struct
		//Wertetyp
		//Wenn ein Struct zugewiesen wird, wird dieser kopiert
		//Wenn zwei Structs verglichen werden, werden die Inhalte verglichen
		int i = 10;
		int j = i;
		i = 20;

		//class
		//Referenztyp
		//Wenn eine Klasse zugewiesen wird, wird diese referenziert
		//Wenn zwei Klassen verglichen werden, werden die Speicheradressen verglichen
		Person p = new Person(10, default, default, default, default, default, default, default, default, default, default, default);
		Person p2 = p; //Referenz auf das Objekt hinter p
					   //p.ID = 20;

		Console.WriteLine(p == p2);
		Console.WriteLine(p.GetHashCode() == p2.GetHashCode());

		//ref
		int r = 10;
		ref int q = ref r; //Stellt eine Referenz auf r her
		r = 20;

		Console.WriteLine();

		Test(0, 5, 0);
		Test(b: 5);

		string zahl = r switch
		{
			0 => "Null",
			1 => "Eins",
			2 => "Zwei",
			_ => "Andere Zahl"
		};

		switch (r)
		{
			case 1:
				zahl = "Eins";
				break;
			case 2:
				zahl = "Zwei";
				break;
			case 3:
				zahl = "Drei";
				break;
			case > 4 and < 10:
				zahl = "zw. 4 und 10";
				break;
			default:
				zahl = "Andere Zahl";
				break;
		}

		//Externe Resourcen (Files, APIs, DBs) werden vom GC nicht automatisch freigegeben
		//Diese müssen manuell geschlossen werden
		using (StreamWriter sw = new StreamWriter("File.txt"))
		{
			sw.WriteLine(zahl);
		} //hier automatisch .Dispose()
		  //sw.Dispose(); //Gibt die Resourcen wieder frei

		using HttpClient client = new HttpClient(); //Bleibt geöffnet bis zum Ende der Methode

		//Null-Coalescing Operator (??-Operator): Wenn die linke Seite nicht null ist, nimm die linke Seite, sonst die rechte Seite
		List<int> zahlen = new List<int>();
		if (zahlen == null)
			zahlen = new List<int>();

		zahlen = zahlen == null ? new List<int>() : zahlen;

		zahlen = zahlen ?? new List<int>();

		zahlen ??= new List<int>();

		zahlen ??= new(); //ab C# 10

		zahlen ??= []; //ab C# 12

		//Verbatim-String (@-String): Ignoriert Escape-Sequenzen
		string pfad = @"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\9.0.5\System.Security.Principal.Windows.dll";

		Person p3 = new();
		var p4 = new Person();

		IntList test = [];
		test.Add(1);
		test.Add(2);
		test.Order();
		test.Clear();

		DateTime dt = DateTime.Now;
		dt += TimeSpan.FromDays(3);

		double a = 0;
		int b = 1; //double und int haben keine gemeinsame Oberklasse -> Inkompatibel
		a = b; //Implizite Konvertierung
		b = (int) a; //Explizite Konvertierung
	}

	public static void Test(int a = 0, int b = 0, int c = 0)
	{
		Console.WriteLine("Hallo Welt");
	}

	public static void Test2(IDisposable d)
	{
		d.Dispose();
	}
}

public record Person(int ID, string Name, string Description, string Address, string City, string Region, string PostalCode, string Country, string Phone, string Fax, string HomePage, string Extension)
{
	public Person() : this(null) { }
}

//public class Person
//{
//	public int ID { get; set; }
//	public string Name { get; set; }
//	public string Description { get; set; }
//	public string Address { get; set; }
//	public string City { get; set; }
//	public string Region { get; set; }
//	public string PostalCode { get; set; }
//	public string Country { get; set; }
//	public string Phone { get; set; }
//	public string Fax { get; set; }
//	public string HomePage { get; set; }
//	public string Extension { get; set; }
//}

public class Fahrzeug(int maxV, FahrzeugMarke marke)
{
	public int MaxV { get; set; } = maxV;

	public FahrzeugMarke Marke { get; set; } = marke;
}

public enum FahrzeugMarke { Audi, BMW, VW }