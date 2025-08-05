namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		Task t2 = Task.Factory.StartNew(Run); //Wird automatisch gestartet

		Task t3 = Task.Run(Run); //Wird automatisch gestartet

		//Ab hier parallel

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		//Hier wird der Task abgebrochen, wenn der Main Thread fertig ist
		//Siehe Multithreading Hintergrund-/Vordergrundthreads
		//Lösung: Main Thread blockieren
		Console.ReadKey();
		//t.Wait();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Task: {i}");
		}
	}
}
