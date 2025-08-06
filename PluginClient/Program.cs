using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		//Hier werden später die Plugins geladen
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2025_08_04\PluginCalculator\bin\Debug\net9.0\PluginCalculator.dll";
		Assembly a = Assembly.LoadFrom(pfad);
		Type p = a.GetTypes().First(e => e.GetInterface(nameof(IPlugin)) != null);
		IPlugin plugin = (IPlugin) Activator.CreateInstance(p);

		Console.WriteLine($"Name: {plugin.Name}");
		Console.WriteLine($"Beschreibung: {plugin.Description}");
		Console.WriteLine($"Autor: {plugin.Author}");
		Console.WriteLine($"Version: {plugin.Version}");

		MethodInfo[] methods = p.GetMethods().Where(e => e.GetCustomAttribute<ReflectionVisible>() != null).ToArray();
		Console.WriteLine($"Wähle eine Methode aus (0-{methods.Length}): ");
		for (int i = 0; i < methods.Length; i++)
		{
			Console.WriteLine($"{i}: {methods[i].Name}");
		}
		int x = int.Parse(Console.ReadLine());
		methods[x].Invoke(plugin, null);
	}
}
