namespace Multitasking;

internal class _04_TaskWarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		t.Wait(); //selbes Problem wie t.Result

		Task.WaitAll(t); //Wartet auf mehrere Tasks

		Task.WaitAny(t); //Wartet auf den schnellsten Task

		//Schleife wird nicht gestartet, bis der Task fertig ist
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Task: {i}");
		}
	}
}
