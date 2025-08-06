using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		//Reflection
		//Arbeiten mit Objekten zur Laufzeit, deren Typ nicht bekannt ist
		
		//Alles geht von einem Type Objekt aus

		//1. GetType()
		Program p = new Program();
		Type pt = p.GetType();

		//2. typeof
		Type t = typeof(Program);

		//Über Methoden des Type Objekts kann das Objekt analysiert werden
		MethodInfo[] mi = pt.GetMethods();
		PropertyInfo[] pi = pt.GetProperties();
		FieldInfo[]	fi = pt.GetFields();
		ConstructorInfo[] ci = pt.GetConstructors();
		EventInfo[] ev = pt.GetEvents();

		//Methoden können verwendet werden, Properties können beschrieben werden, Events können angehängt werden, ...
		
		//Objekte ohne new erstellen: Activator
		object program = Activator.CreateInstance(pt);
		object program2 = Activator.CreateInstance("Reflection", "Reflection.Program");

		pt.GetProperties()
			.First(e => e.PropertyType == typeof(string)) //Finde das erste string Property
			.SetValue(program, "World"); //Schreibe einen Wert in dieses Property

		pt.GetMethod("TestMethod")
			.Invoke(program, null);

		//DLL laden
		//Externe Methoden ansprechen: DLL laden (Assembly), Keyword extern
		Assembly a = Assembly.GetExecutingAssembly(); //Das jetztige Projekt

		//Aufgabe: Component aus DelegatesEvents laden und ausführen
		Assembly e = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2025_08_04\DelegatesEvents\bin\Debug\net9.0\DelegatesEvents.dll");
		Type ct = e.GetType("DelegatesEvents.Component");
		object comp = Activator.CreateInstance(ct);
		ct.GetEvent("Start").AddEventHandler(comp, Start);
		ct.GetEvent("End").AddEventHandler(comp, (object sender, EventArgs a) => Console.WriteLine("Reflection End"));
		ct.GetEvent("Progress").AddEventHandler(comp, (object sender, int a) => Console.WriteLine($"Reflection Fortschritt: {a}"));
		ct.GetMethod("DoWork").Invoke(comp, null);
	}

	public string Test { get; set; }

	public void TestMethod()
	{
		Console.WriteLine($"Hello {Test}");
	}

	public static void Start(object sender,  EventArgs e)
	{
		Console.WriteLine("Reflection Start");
	}
}