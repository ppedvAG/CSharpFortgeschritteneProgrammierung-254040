namespace DelegatesEvents;

internal class Delegates
{
	/// <summary>
	/// Definition eines Delegates (sollte in einem eigenen File liegen)
	/// </summary>
	public delegate void Vorstellung(string name);

	static void Main(string[] args)
	{
		//Delegates
		//Eigener Typ, welcher einen Methodenaufbau hat
		//Wird später erstellt (new), und erhält Methodenzeiger als Parameter

		Vorstellung v = new Vorstellung(VorstellungDE); //Erstellung des Delegates mit Initialmethode
		v("Max"); //Ausführung des Delegates

		v += VorstellungEN; //Methodenzeiger anhängen mit += (kein new notwendig)
		v += VorstellungEN; //Methodenzeiger anhängen mit += (kein new notwendig)
		v += VorstellungEN; //Methodenzeiger anhängen mit += (kein new notwendig)
		v("Tim");

		v -= VorstellungDE; //Methodenzeiger abnehmen
		v -= VorstellungDE; //Wenn die Methode nicht angehängt ist, passiert nichts
		v -= VorstellungDE;
		v("Udo");

		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN; //Delegates werden null, wenn sie leer sind
		//v("Max");

		if (v is not null)
			v("Max");

		v?.Invoke("Max"); //Null propagation: Führe die Methode nur aus, wenn die Variable nicht null ist

		foreach (Delegate dg in v.GetInvocationList()) //Delegate iterieren
		{
			Console.WriteLine(dg.Method.Name);
		}
	}

	static void VorstellungDE(string name) => Console.WriteLine($"Hallo mein Name ist {name}");

	static void VorstellungEN(string name) => Console.WriteLine($"Hello my name is {name}");
}
