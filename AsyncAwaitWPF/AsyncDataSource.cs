namespace AsyncAwaitWPF;

class AsyncDataSource
{
	public async IAsyncEnumerable<int> Generate()
	{
		for (int i = 0; ; i++)
		{
			await Task.Delay(Random.Shared.Next(100, 500));
			yield return i;
		}
	}
}
