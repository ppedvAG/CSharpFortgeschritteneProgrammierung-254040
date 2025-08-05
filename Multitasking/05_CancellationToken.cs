using System.Data.Common;

namespace Multitasking;

internal class _05_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new(); //Source erstellen
		CancellationToken token = cts.Token; //Token aus der Source entnehmen; Tokens sind structs -> Kopien erstellt

		Task t = new Task(Run, token);
		t.Start();

		Thread.Sleep(500);
		cts.Cancel();

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				//if (ct.IsCancellationRequested)
				//	break;

				ct.ThrowIfCancellationRequested(); //Wenn ein Task eine Exception wirft, stürzt das Programm nicht ab

				Console.WriteLine($"Task: {i}");
				Thread.Sleep(10);
			}
		}
	}
}
