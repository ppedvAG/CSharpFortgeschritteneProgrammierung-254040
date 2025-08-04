namespace DelegatesEvents;

internal class ActionFunc
{
	static void Main(string[] args)
	{
		//Action und Func
		//Zwei vordefinierte Delegates, welche an vielen Stellen in C# vorkommen
		//Essentiell für die Fortgeschrittene Programmierung
		//Beispiele: TPL, Linq, Reflection, ...

		//Action: Delegate, welches einen Methodenzeiger hält, der void zurückgibt und 0-16 Parameter hat
		Action a = new Action(Test);
		a += Test;
		a -= Test;
		a(); //Alles möglich wie gerade gehabt

		Action<int, int> add = Addiere;
		add?.Invoke(3, 4);

		//Verwendung: Methode mit Action Parameter
		//-> Methode konfigurierbar machen
		Benachrichtigung(5, Console.WriteLine);
		//Benachrichtigung(5, Logger.Log);
		//Benachrichtigung(5, TextBlock.Text);
		
		List<int> list = [1, 2, 3, 4, 5];
		list.ForEach(Console.WriteLine);
		list.ForEach(Mal2);

		void Mal2(int x) => Console.WriteLine(x * 2);

		////////////////////////////////////////////////////////////////////////////

		//Func: Delegate, welches einen Methodenzeiger hält, der TResult zurückgibt und 0-16 Parameter hat
		//WICHTIG: Letzter Generic ist der Rückgabedatentyp
		Func<int> f = Test2;
		int x = f();

		int? i = f?.Invoke();
		int j = f?.Invoke() ?? int.MinValue;

		Func<int, int, double> div = Dividiere;
		Console.WriteLine(div(6, 4));

		//Verwendung: Methode mit Func Parameter
		//-> Methode konfigurierbar machen

		list.Where(TeilbarDurch2); //Aufgabe: Alle Zahlen finden, welche durch 2 teilbar sind

		////////////////////////////////////////////////////////////////////////////

		//Anonyme Funktionen: Methodenzeiger, welche nicht dediziert erstellt werden, sondern nur bei dem Action-/Funcparameter eingesetzt werden
		//-> werden einmal verwendet und weggeworfen
		div = delegate (int x, int y)
		{
			return (double) x / y;
		};

		div += (int x, int y) =>
		{
			return (double) x / y;
		};

		div += (int x, int y) => (double) x / y;

		div += (x, y) => (double) x / y;

		list.ForEach(e => Console.WriteLine(e)); //Action<int>: Methode mit void und einem int Parameter (e: int, =>: Body)
		list.Where((e) => e % 2 == 0); //Func<int, bool>: Methode mit bool als Rückgabe und einem int Parameter (e: int, =>: Body)

		Task t = new Task(() => Console.WriteLine()); //Anonyme Methode ohne Parameter
		list.Aggregate((x, y) => 1); //Anonyme Methode mit 2 Parametern

		//Überall wo Delegates eingesetzt werden, können Lambda Expressions eingesetzt werden
	}

	#region Action
	public static void Test() => Console.WriteLine();

	public static void Addiere(int a, int b) => Console.WriteLine($"{a} + {b} = {a + b}");

	public static void Benachrichtigung(int x, Action<int> a)
	{
		//...
		a?.Invoke(x);
	}
	#endregion

	#region Func
	public static int Test2() => Random.Shared.Next();

	public static double Dividiere(int a, int b) => (double) a / b;

	public static bool TeilbarDurch2(int a) => a % 2 == 0;
	#endregion
}
