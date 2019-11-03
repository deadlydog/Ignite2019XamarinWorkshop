using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MonkeyFinder.ViewModel
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		#region NotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName]string name = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		#endregion

		public BaseViewModel()
		{

		}

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				if (_title == value)
					return;

				_title = value;
				OnPropertyChanged();
			}
		}

		private bool _isBusy;
		public bool IsBusy
		{
			get => _isBusy;
			set
			{
				if (_isBusy == value)
					return;

				_isBusy = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(IsNotBusy));
			}
		}

		public bool IsNotBusy => !IsBusy;
	}
}
