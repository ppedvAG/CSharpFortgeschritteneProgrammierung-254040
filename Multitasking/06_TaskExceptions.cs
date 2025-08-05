namespace Multitasking;

internal class _06_TaskExceptions
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		Task<int> t2 = new Task<int>(Run2);
		t2.Start();

		try
		{
			//Über t.Wait, WaitAll und t.Result können die Exceptions der Tasks gefangen werden
			//t.Wait();
			Task.WaitAll(t, t2);
			//Console.WriteLine(t2.Result);
		}
		catch (AggregateException ex)
		{
			foreach (Exception e in ex.InnerExceptions)
			{
				Console.WriteLine(e.Message);
			}
		}

		Console.ReadKey();
	}

	public static void Run()
	{
		Thread.Sleep(500);
		throw new NotImplementedException();
	}

	public static int Run2()
	{
		Thread.Sleep(1000);
		throw new ArgumentException();
	}
}
