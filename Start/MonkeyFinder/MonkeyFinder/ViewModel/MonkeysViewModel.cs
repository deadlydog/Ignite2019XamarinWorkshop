using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

using System.Linq;
using MonkeyFinder.Model;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Collections.Generic;

namespace MonkeyFinder.ViewModel
{
	public class MonkeysViewModel : BaseViewModel
	{
		public ObservableCollection<Monkey> Monkeys { get; }

		public Command GetMonkeysCommand { get; set; }

		public MonkeysViewModel()
		{
			Title = "Monkey Finder";
			Monkeys = new ObservableCollection<Monkey>();
			GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
		}

		HttpClient httpClient;
		HttpClient Client => httpClient ?? (httpClient = new HttpClient());

		async Task GetMonkeysAsync()
		{
			if (IsBusy)
				return;

			try
			{
				IsBusy = true;

				var json = await Client.GetStringAsync("https://montemagno.com/monkeys.json");
				var monkeys = Monkey.FromJson(json);

				Monkeys.Clear();
				foreach (var monkey in monkeys)
					Monkeys.Add(monkey);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
				await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
