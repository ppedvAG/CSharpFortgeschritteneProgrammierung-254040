namespace LinqErweiterungsmethoden;

internal static class ExtensionMethods
{
	public static int Quersumme(this int x)
	{
		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
	{
		return list.OrderBy(e => Random.Shared.Next());
	}
}
