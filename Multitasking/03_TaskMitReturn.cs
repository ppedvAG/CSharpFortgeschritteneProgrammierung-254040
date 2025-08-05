namespace Multitasking;

internal class _03_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Run);
		t.Start();

		//Console.WriteLine(t.Result); //Ergebnis entnehmen

		bool resultPrinted = false;
		for (int i = 0; i < 100; i++)
		{
			//Weiteres Problem: Die Ausgabe wird erst erscheinen, wenn Thread.Sleep(25) fertig ist
			if (t.IsCompletedSuccessfully && !resultPrinted)
			{
				Console.WriteLine(t.Result);
				resultPrinted = true;
			}

			Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(25);
		}

		//Console.WriteLine(t.Result);

		Console.ReadKey();
	}

	static int Run()
	{
		Thread.Sleep(1000);
		return Random.Shared.Next();
	}
}
