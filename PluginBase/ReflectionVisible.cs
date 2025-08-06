namespace PluginBase;

[AttributeUsage(AttributeTargets.Method)]
public class ReflectionVisible : Attribute
{
	public string Name { get; }

	public ReflectionVisible(string name)
	{
		Name = name;
	}
}
