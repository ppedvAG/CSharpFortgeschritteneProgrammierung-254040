using PluginBase;

namespace PluginCalculator;

public class Calculator : IPlugin
{
	public string Name => "Rechner Plugin";

	public string Description => "Ein einfacher Rechner";

	public string Author => "Lukas";

	public string Version => "1.0";

	[ReflectionVisible("Add")]
	public double Addiere(int x, int y) => x + y;

	[ReflectionVisible("Sub")]
	public double Subtrahiere(int x, int y) => x - y;
}
