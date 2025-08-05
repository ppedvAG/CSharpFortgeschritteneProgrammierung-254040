namespace Multitasking;

internal class _02_TaskMitParameter
{
	static void Main(string[] args)
	{
		int x = 200;
		Task t = new Task(Run, x); //WICHTIG: Der Parameter muss vom Typ object sein
		t.Start();

		//Task.Run kann keine Daten empfangen -> new Task notwendig

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
			{
				Console.WriteLine($"Task: {i}/{x}");
			}
		}
	}
}

/// <summary>
/// Mehrere Daten mitgeben
/// </summary>
public record TaskData(int X, string Str, bool TF);