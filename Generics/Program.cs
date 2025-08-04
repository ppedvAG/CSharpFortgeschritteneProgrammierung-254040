using System.Collections;

namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		//Generic: Platzhalter für Typ
		//Wird bei Verwendung innerhalb der Klasse/Methode ersetzt durch den gegebenen Typen
		List<string> list = new List<string>();
		list.Add("a"); //Alle T's in der Klasse werden durch string ersetzt

		List<int> list2 = new List<int>();
		list2.Add(1); //Alle T's in der Klasse werden durch int ersetzt

		Dictionary<int, string> dict = []; //Klasse mit 2 Generics

		////////////////////////////////////////////

		Test(1); //Generic wird automatisch von dem Parameter gefüllt (hier int)

		list.Where(e => e.StartsWith("a")); //Generic wird automatisch von dem Parameter gefüllt (hier string)
	}

	/// <summary>
	/// Bei Generischen Methoden wird generell ein Parameter oder Rückgabewert vom generischen Typen eingesetzt
	/// </summary>
	static T Test<T>(T obj)
	{
		Console.WriteLine(typeof(T)); //Gibt den Typen des Generics zurück
		Console.WriteLine(nameof(T)); //Gibt den Namen des Typen als String zurück
		Console.WriteLine(default(T)); //gibt den Standardwert des Typens zurück (0, null, false)

		//default Beispiel
		if (obj != null)
			return obj;
		else
			return default;
	}
}

public class DataStore<T> : IEnumerable<T>
{
	private T[] _data;

	public List<T> Data => _data.ToList();

	public void Add(T obj, int index)
	{
		_data[index] = obj;
	}

	public T Get(int index)
	{
		return _data[index];
	}

	public IEnumerator<T> GetEnumerator()
	{
		foreach (T obj in _data)
			yield return obj;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public T this[int index]
	{
		get => _data[index];
		set => _data[index] = value;
	}
}