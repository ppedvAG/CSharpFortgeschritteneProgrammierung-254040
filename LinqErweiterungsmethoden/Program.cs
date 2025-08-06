using System.Diagnostics;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Listentheorie
		//IEnumerable
		//Ist ein Interface
		//Basis von allen Listen (List, Array, Dictionary, ...)
		//Ermöglicht foreach-Schleifen und Linq

		//IEnumerable hält keine Daten
		//IEnumerable ist eine Anleitung zum Erstellen der Daten
		IEnumerable<int> x = Enumerable.Range(0, 1_000_000_000); //Nur eine Anleitung <1ms, 0B RAM

		//List<int> y = Enumerable.Range(0, 1_000_000_000).ToList(); //Erstellung der Daten ~750ms, 4GB RAM

		x.Where(e => e % 2 == 0); //Die meisten Linq-Funktionen geben als Ergebnis ein IEnumerable zurück

		//Aufbau
		//Eine Methode: GetEnumerator; gibt einen IEnumerator zurück
		//IEnumerator:
		//- bool MoveNext(): Bewegt den Zeiger um ein Element weiter
		//- void Reset(): Setzt den Zeiger an den Anfang zurück
		//- T Current: Gibt das Element zurück, auf welches der Zeiger zeigt

		List<int> zahlen = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
		foreach (int i in zahlen)
		{
			Console.WriteLine(i);
		}

		//Schleife ohne Schleife
		IEnumerator<int> enumerator = zahlen.GetEnumerator();
		start:
		bool hasNext = enumerator.MoveNext();
		if (hasNext)
		{
			Console.WriteLine(enumerator.Current);
			goto start;
		}
		enumerator.Reset();

		//Stichwort: Deferred Execution
		#endregion

		#region Einfaches Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

		ints.Average(); //Alle Linq Methoden sind Erweiterungsmethoden
		ints.Sum(); //Gekennzeichnet durch Würfel + Pfeil
		ints.Min();
		ints.Max();

		ints.First(); //Erstes Element, Exception wenn kein Element gefunden wird
		ints.FirstOrDefault(); //Erstes Element, default(T) wenn kein Element gefunden wird

		//Suche die erste Zahl die durch 50 teilbar ist
		//ints.First(e => e % 50 == 0); //Exception
		ints.FirstOrDefault(e => e % 50 == 0); //Exception
		#endregion

		#region Linq mit Objekten
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

		//Finde alle VWs
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW);

		//Finde alle VWs mit mind. 200km/h
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW).Where(e => e.MaxV > 200);
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW && e.MaxV > 200);

		//OrderBy
		//Finde alle VWs, sortiert nach Geschwindigkeit
		fahrzeuge
			.Where(e => e.Marke == FahrzeugMarke.VW)
			.OrderBy(e => e.MaxV);

		fahrzeuge
			.Where(e => e.Marke == FahrzeugMarke.VW)
			.OrderByDescending(e => e.MaxV);

		//Sortiere nach Marke und nach Geschwindigkeit
		//Subsequente Sortierung mittels ThenBy(Descending)
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);

		//All & Any
		//Prüft ob alle/mind. ein Element(e) die Bedingung erfüllen/erfüllt

		//Fahren alle Fahrzeuge mind. 200km/h?
		if (fahrzeuge.All(e => e.MaxV > 200)) { }

		//Fährt mind. ein Fahrzeuge über 200km/h?
		if (fahrzeuge.Any(e => e.MaxV > 200)) { }

		//Besteht ein string nur aus Buchstaben?
		string text = "Hallo Welt";
		if (text.All(char.IsLetter))
		{

		}

		//Wieviele BMWs haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.BMW);
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).Count();

		//Average, Min, Max, Sum
		//Was ist die Durchschnittsgeschwindigkeit aller Fahrzeuge?
		fahrzeuge.Average(e => e.MaxV); //208.4166666666666
		fahrzeuge.Sum(e => e.MaxV);
		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit
		fahrzeuge.Max(e => e.MaxV);

		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit
		fahrzeuge.MaxBy(e => e.MaxV);

		//Finde die 3 schnellsten Fahrzeuge
		fahrzeuge
			.OrderByDescending(e => e.MaxV)
			.Take(3);

		//Webshop
		int page = 0;
		fahrzeuge.Skip(page * 10).Take(10);

		//GroupBy
		//Gruppiert anhand eines Kriterium
		//Erstellt eine Gruppe pro Element im Kriterium, und legt alle Daten in die Gruppen hinein die dem Kriterium entsprechen
		fahrzeuge.GroupBy(e => e.Marke); //Audi-Gruppe, BMW-Gruppe, VW-Gruppe

		Dictionary<FahrzeugMarke, IEnumerable<Fahrzeug>> dict = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(k => k.Key, v => v.AsEnumerable()); //IGrouping ist ein IEnumerable -> ToList(), ToArray(), ... möglich

		//Select
		//Wandelt die Liste in eine andere Form um
		//In der Lambda-Expression kann eine Form angegeben werden, jedes Element wird zu dieser Form konvertiert

		//2 Anwendungsfälle

		//1. Anwendung (80%): Einzelnes Feld aus der Liste entnehmen

		//Welches Marken haben wir?
		fahrzeuge
			.Select(e => e.Marke)
			.Distinct();

		//Aufgabe: Gib zu jeder Person die volle Adresse zurück
		List<Person> p = [];
		p.Select(e => $"{e.City} {e.Region}, {e.PostalCode} {e.Country}"); //Eine String Liste

		fahrzeuge.Select(e => e.MaxV).Average();

		//2. Anwendung (20%): Transformation der Liste

		//Liste von Dateinamen ohne Endung in einem Ordner

		//Ohne Linq
		string[] pfade = Directory.GetFiles(@"C:\Windows");
		List<string> pfadeOhneEndung = [];
		foreach (string pfad in pfade)
		{
			pfadeOhneEndung.Add(Path.GetFileNameWithoutExtension(pfad));
		}

		List<string> pe = Directory.GetFiles(@"C:\Windows").Select(Path.GetFileNameWithoutExtension).ToList();
		
		bool b = pfadeOhneEndung.SequenceEqual(pe);

		//Aus einem string die char-Codes entnehmen
		string t = "Hallo Welt";
		t.Select(e => (int) e);

		//Eine Liste vom Kommazahlen mit 0.1er Schritten
		Enumerable.Range(0, 100).Select(e => e / 10.0);
		#endregion

		#region Erweiterungsmethoden
		int z = 1384;
		z.Quersumme();

		//Vom Compiler generiert
		ExtensionMethods.Quersumme(z);

		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW); //Linq
		Enumerable.Where(fahrzeuge, e => e.Marke == FahrzeugMarke.VW); //Übersetzt durch den Compiler

		//Eigene Linq Methode
		fahrzeuge
			.Where(e => e.Marke == FahrzeugMarke.VW)
			.Shuffle();
		#endregion
	}
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]
public class Fahrzeug(int maxV, FahrzeugMarke marke)
{
	public int MaxV { get; set; } = maxV;

	public FahrzeugMarke Marke { get; set; } = marke;
}

public enum FahrzeugMarke { Audi, BMW, VW }

public record Person(int ID, string Name, string Description, string City, string Region, string PostalCode, string Country, string Phone, string Fax, string HomePage, string Extension)
{
	public Person() : this(null) { }
}