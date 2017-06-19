using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EmbeddedFormsDemo.Models;
using EmbeddedFormsDemo.Services;

namespace EmbeddedFormsDemo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public const string LoginMessage = "login";

		private readonly JsonPlaceholderApi api;

		private string errorMessage;

		public LoginPage()
		{
			InitializeComponent();

			// initialize our services
			api = new JsonPlaceholderApi();

			// set up the data binding
			LoginCommand = new Command(OnLogin);
			BindingContext = this;
		}

		private async void OnLogin()
		{
			IsBusy = true;

			// reset errors
			ErrorMessage = "";

			// try logging in with our services
			var user = await api.LoginAsync(UserName, Password);

			// if there was an error
			if (user == null)
			{
				ErrorMessage = "There was a problem logging in, make sure you have entered a username and password. Any username and password.";
			}
			else
			{
				// let the app know we are finished
				MessagingCenter.Send(user, LoginMessage);
			}

			IsBusy = false;
		}

		public string ErrorMessage
		{
			get { return errorMessage; }
			set
			{
				errorMessage = value;
				OnPropertyChanged();
			}
		}

		// we don't want to save this as the user types
		public string UserName { get; set; }

		// we don't want to save this at all
		public string Password { get; set; }

		public ICommand LoginCommand { get; }
	}
}
