using System.Diagnostics;
using System.Net;

namespace AsyncAwait;

internal class Program
{
	public static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();

		//Synchron
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		///////////////////////////////////////////////////////////////////////

		//Tasks
		//Task t1 = Task.Run(Toast);
		//Task t2 = new Task(Tasse);
		//t2.ContinueWith(v => Kaffee());
		//t2.Start();
		//Task.WaitAll(t1, t2); //WaitAll funktioniert in Konsole, funktioniert nicht in GUI
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s
		//Console.ReadKey();

		//GUI-funktionale Variante
		//Großer Aufwand ohne async/await
		//bool hasPrinted = false;
		//Task t1 = new Task(Toast);
		//t1.ContinueWith(v =>
		//{
		//	if (!hasPrinted)
		//	{
		//		Console.WriteLine(sw.ElapsedMilliseconds);
		//		hasPrinted = true;
		//	}
		//});
		//t1.Start();
		//Task t2 = new Task(Tasse);
		//t2.ContinueWith(v => Kaffee());
		//t2.Start();
		//Console.ReadKey();

		///////////////////////////////////////////////////////////////////////

		//async/await

		//Checkliste
		//1. Aufgabe starten (Task.Run)
		//2. Zwischenschritte (User informieren, ...)
		//3. Warten (await)

		//await
		//Effektiv gleich wie task.Wait(), aber kein blockieren
		//WICHTIG: Kann nur in einer async Methode verwendet werden
		//Task t1 = Task.Run(Toast); //Aufgabe starten
		//Task t2 = Task.Run(Tasse); //Aufgabe starten
		//await t2; //Warten
		//Task t3 = Task.Run(Kaffee); //Aufgabe starten
		//await t3; //Warten
		//await t1; //Warten
		//Console.WriteLine(sw.ElapsedMilliseconds);
		//Tasks an sich können awaited werden
		//-> alle langandauernden Operationen können so parallelisiert werden

		//async
		//Wenn eine Methode als async gekennzeichnet wird, kann diese await benutzen
		//Wenn eine async Methode ausgeführt wird, wird diese als Task ausgeführt

		//Task t1 = ToastAsync();
		//Task t2 = TasseAsync();
		//await t2;
		//Task t3 = KaffeeAsync();
		//await Task.WhenAll(t1, t3); //Mehrere Tasks gleichzeitig awaiten
		//Console.WriteLine(sw.ElapsedMilliseconds);

		//Rückgabewerte bei async Methoden
		//async void: Kann selbst await verwenden, kann aber selbst nicht awaited werden
		//async Task: Kann selbst await verwenden und kann selbst awaited werden
		//async Task<T>: Kann selbst await verwenden, kann selbst awaited werden und gibt ein Ergebnis zurück

		///////////////////////////////////////////////////////////////////////

		//async mit Objekten
		//Task<Toast> toast = ToastObjectAsync();
		//Task<Tasse> tasse = TasseObjectAsync();
		//Tasse t = await tasse; //await gibt einen Wert zurück, wenn der Task einen Wert zurückgibt (t.Result)
		//Task<Kaffee> kaffee = KaffeeObjectAsync(t);
		//Kaffee k = await kaffee;
		//Toast t2 = await toast;
		//Fruehstueck f = new Fruehstueck(t2, k);
		//Console.WriteLine(sw.ElapsedMilliseconds);

		//Vereinfachen
		Task<Toast> toast = ToastObjectAsync();
		Task<Tasse> tasse = TasseObjectAsync();
		Task<Kaffee> kaffee = KaffeeObjectAsync(await tasse);
		Fruehstueck f = new Fruehstueck(await toast, await kaffee);
		Console.WriteLine(sw.ElapsedMilliseconds);

		Console.ReadKey();
	}

	#region Synchron
	public static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	public static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	public static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Async
	public static async Task ToastAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		//Diese Methode ist nicht void, und hat keinen Rückgabewert
	}

	public static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
	}

	public static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Async mit Objekten
	public static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	public static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	public static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public record Toast;

public record Tasse;

public record Kaffee(Tasse t);

public record Fruehstueck(Toast t, Kaffee k);