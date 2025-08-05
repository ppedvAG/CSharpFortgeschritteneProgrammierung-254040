namespace Multitasking;

internal class _08_Lock
{
	public static int Counter;

	public static object Lock { get; set; } = new();

	static void Main(string[] args)
	{
		List<Task> tasks = [];
		for (int i = 0; i < 100; i++)
			tasks.Add(Task.Run(Increment));
		Console.ReadKey();
	}

	static void Increment()
	{
		for (int i = 0; i < 100; i++)
		{
			//lock-Block: Sperrt den gegebenen Bereich, sobald ein Task Code innerhalb des Blocks ausführt
			lock (Lock) //Das Lock-Object hält die Information, welcher Task gerade diesen Block ausgeführt
			{
				Counter++;
				Console.WriteLine(Counter);
			}

			//Monitor: Identisch zu Lock-Block, aber mit 2 Methoden (if's möglich)
			//Monitor.Enter(Lock);
			//Counter++;
			//Console.WriteLine(Counter);
			//Monitor.Exit(Lock);

			Interlocked.Add(ref Counter, 1);
			Console.WriteLine(Counter);
		}
	}
}
