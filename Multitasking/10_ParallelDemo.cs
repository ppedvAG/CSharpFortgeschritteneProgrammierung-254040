using System.Diagnostics;

namespace Multitasking;

public class _10_ParallelDemo
{
	static void Main(string[] args)
	{
		int[] iterations = [1000, 10_000, 50_000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000];
		foreach (int d in iterations)
		{
			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(d);
			sw.Stop();
			Console.WriteLine($"For Interations: {d}, {sw.ElapsedMilliseconds}ms");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(d);
			sw2.Stop();
			Console.WriteLine($"ParallelFor Interations: {d}, {sw2.ElapsedMilliseconds}ms");

			Console.WriteLine("------------------------------------------------------------");
		}

		/*
			For Interations: 1000, 0ms
			ParallelFor Interations: 1000, 25ms
			------------------------------------------------------------
			For Interations: 10000, 24ms
			ParallelFor Interations: 10000, 0ms
			------------------------------------------------------------
			For Interations: 50000, 2ms
			ParallelFor Interations: 50000, 32ms
			------------------------------------------------------------
			For Interations: 100000, 4ms
			ParallelFor Interations: 100000, 1ms
			------------------------------------------------------------
			For Interations: 250000, 48ms
			ParallelFor Interations: 250000, 3ms
			------------------------------------------------------------
			For Interations: 500000, 19ms
			ParallelFor Interations: 500000, 28ms
			------------------------------------------------------------
			For Interations: 1000000, 68ms
			ParallelFor Interations: 1000000, 70ms
			------------------------------------------------------------
			For Interations: 5000000, 196ms
			ParallelFor Interations: 5000000, 82ms
			------------------------------------------------------------
			For Interations: 10000000, 393ms
			ParallelFor Interations: 10000000, 107ms
			------------------------------------------------------------
			For Interations: 100000000, 4464ms
			ParallelFor Interations: 100000000, 1659ms
			------------------------------------------------------------
		 */
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i =>
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}