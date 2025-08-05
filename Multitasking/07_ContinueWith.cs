namespace Multitasking;

internal class _07_ContinueWith
{
	static void Main(string[] args)
	{
		//Probleme: Ergebnis kam mittendrin nur mit hohem Aufwand; Fehlerbehandlung kam immer gleichzeitig bei allen Tasks
		//Lösung: ContinueWith

		//ContinueWith
		//Sobald t1 fertig ist, wird ein weiterer Task gestartet

		Task t = new Task(Run);
		t.ContinueWith(ContinueRun); //Bei ContinueWith haben wir immer Zugriff auf den vorherigen Task
		t.ContinueWith(vorherigerTask => ContinueRun(vorherigerTask)); //Mit Lambda-Expression
		t.Start();

		//Problem: Main Thread wird von t.Wait() blockiert
		//t.Wait();
		//Task t2 = new Task(ContinueRun);
		//t2.Start();

		//////////////////////////////////////////////////////////////////

		Task<int> r = new Task<int>(Result);
		r.ContinueWith(v => Console.WriteLine(v.Result));
		r.Start();

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(25);
		}

		//////////////////////////////////////////////////////////////////

		List<Task> tasks = [];
		for (int i = 0; i < 50; i++)
		{
			Task e = new Task(Exception);
			//ContinueWith mit Bedingung
			e.ContinueWith(v => Console.WriteLine("Erfolg"), TaskContinuationOptions.OnlyOnRanToCompletion); //Erfolg
			e.ContinueWith(v => Console.WriteLine(v.Exception.Message), TaskContinuationOptions.OnlyOnFaulted); //Fehler
			e.Start();
			tasks.Add(e);
		}
		Task.WaitAll(tasks);

		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Task: {i}");
			Thread.Sleep(10);
		}
	}

	static void ContinueRun(Task t)
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Continuation Task: {i}");
			Thread.Sleep(10);
		}
	}

	static int Result()
	{
		Thread.Sleep(500);
		return Random.Shared.Next();
	}

	static void Exception()
	{
		Thread.Sleep(Random.Shared.Next(500, 1000));
		if (Random.Shared.Next() % 2 == 0)
			throw new InvalidDataException(DateTime.Now.Millisecond.ToString());
	}
}
