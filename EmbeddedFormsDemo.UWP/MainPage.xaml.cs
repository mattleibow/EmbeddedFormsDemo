using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

using Page = Windows.UI.Xaml.Controls.Page;
using Frame = Windows.UI.Xaml.Controls.Frame;

using EmbeddedFormsDemo.Models;
using EmbeddedFormsDemo.Views;

namespace EmbeddedFormsDemo.UWP
{
	public sealed partial class MainPage : Page, INotifyPropertyChanged
	{
		private string welcomeText;

		public MainPage()
		{
			InitializeComponent();

			// native UWP data binding
			WelcomeText = "Welcome to the app, stranger!";
			DataContext = this;
		}

		public string WelcomeText
		{
			get { return welcomeText; }
			set
			{
				welcomeText = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WelcomeText)));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(User)));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public User User { get; set; }

		private void OnLoginFlyoutOpening(object sender, object e)
		{
			var flyout = sender as Flyout;

			if (!Xamarin.Forms.Forms.IsInitialized)
			{
				// initialize Xamarin.Forms before we use it
				Xamarin.Forms.Forms.Init(App.LastLaunchEventArgs);

				// we want to listen to the messaging center
				Xamarin.Forms.MessagingCenter.Subscribe(this, LoginPage.LoginMessage, (User user) =>
				{
					// update the app
					User = user;
					// show some message for some random reason
					WelcomeText = $"Welcome back {user.Name}!";

					// hide the login screen
					flyout.Hide();
				});
			}

			// create the login page (Xamarin.Forms ContentPage)
			var loginPage = new LoginPage();

			// set the native dialog to contain the shared login
			var loginElement = loginPage.CreateFrameworkElement();
			flyout.Content = new Frame
			{
				Content = loginElement,
				Width = 300,
				Height = 200
			};
		}
	}
}
