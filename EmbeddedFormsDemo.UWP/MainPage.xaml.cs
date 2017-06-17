using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

using Page = Windows.UI.Xaml.Controls.Page;
using Frame = Windows.UI.Xaml.Controls.Frame;

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
			DataContext = this;

			// create the login page (Xamarin.Forms ContentPage)
			var loginPage = new LoginPage();

			// create the login dialog (Native popup)
			var loginDialog = new ContentDialog
			{
				Title = "Login",
				Content = new Frame
				{
					Content = loginPage.CreateFrameworkElement(),
					Width = 400,
					Height = 300
				},
			};

			// attach an event to navigate to the main fragment after logging in
			loginPage.LoggedIn += user =>
			{
				// hide the login screen
				loginDialog.Hide();

				// show some message for some random reason
				WelcomeText = $"Welcome back {user.Name}!";
			};

			// show the login screen
			loginDialog.ShowAsync();
		}

		public string WelcomeText
		{
			get { return welcomeText; }
			set
			{
				welcomeText = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WelcomeText)));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
