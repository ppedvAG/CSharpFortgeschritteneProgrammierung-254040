using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			Output.Text += i + "\n";
			Thread.Sleep(25);
		}
	}

	private void Button_Click_TaskRun(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < 100; i++)
			{
				Dispatcher.Invoke(() => Output.Text += i + "\n");
				Dispatcher.Invoke(() => Scroll.ScrollToEnd());
				Thread.Sleep(25);
			}
		});
	}

	private async void Button_Click_Async(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			Output.Text += i + "\n";
			Scroll.ScrollToEnd();
			await Task.Delay(25); //await Task.Run(() => Thread.Sleep(25));
		}
	}

	private async void Request(object sender, RoutedEventArgs e)
	{
		//Checkliste
		//1. Aufgabe starten
		//2. Zwischenschritte (User informieren, ...)
		//3. Warten (await)

		string url = "http://www.gutenberg.org/files/54700/54700-0.txt";

		using HttpClient client = new();

		Task<HttpResponseMessage> request = client.GetAsync(url); //Starte den Request

		ReqButton.IsEnabled = false; //Zwischenschritt
		Output.Text = "Text wird geladen..."; //Zwischenschritt

		HttpResponseMessage response = await request; //Warte auf das Ende des Requests

		if (response.IsSuccessStatusCode)
		{
			Task<string> read = response.Content.ReadAsStringAsync();

			Output.Text = "Text wird gelesen...";

			string text = await read;

			Output.Text = text;
		}
		ReqButton.IsEnabled = true;
	}

	private async void Button_Click_AsyncDataSource(object sender, RoutedEventArgs e)
	{
		//Bei jedem Schleifendurchlauf wird auf die nächste Zahl gewartet
		//Wenn die Zahl ankommt, wird die Schleife einmal ausgeführt

		AsyncDataSource ads = new();
		await foreach (int item in ads.Generate())
		{
			Output.Text += item + "\n";
		}
	}
}