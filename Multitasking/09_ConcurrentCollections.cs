using System.Collections.Concurrent;

namespace Multitasking;

internal class _09_ConcurrentCollections
{
	static void Main(string[] args)
	{
		//Falls mehrere Tasks Daten in eine Liste hinzufügen wollen, sollte eine ConcurrentCollection verwendet werden
		//Diese Collection locken automatisch

		ConcurrentBag<int> ints = new ConcurrentBag<int>();
		ints.Add(1);
		ints.Add(2);
		ints.Add(3);

		//ConcurrentBag hat u.a. keinen Index
		//Alternative: SynchronizedCollection

		SynchronizedCollection<int> zahlen = []; //NuGet: System.ServiceModel.Primitives
		zahlen.Add(1);
		zahlen.Add(2);
		zahlen.Add(3);

		Console.WriteLine(zahlen[0]);

		ConcurrentDictionary<int, string> dict = [];
		dict.AddOrUpdate(0, "Null", (k, v) => dict[k] = v);
		string exists = dict.GetOrAdd(1, "Eins");
		bool funktioniert = dict.TryAdd(2, "Zwei");
	}
}
