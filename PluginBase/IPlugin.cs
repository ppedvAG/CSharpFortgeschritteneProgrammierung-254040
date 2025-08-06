namespace PluginBase;

/// <summary>
/// Dieses Projekt wird als Dependency in PluginClient und allen Plugins hinzugefügt
/// </summary>
public interface IPlugin
{
	public string Name { get; }

	public string Description { get; }

	public string Author { get; }

	public string Version { get; }
}