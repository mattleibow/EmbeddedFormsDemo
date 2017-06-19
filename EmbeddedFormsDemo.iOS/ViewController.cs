using System;
using UIKit;
using Xamarin.Forms;

using EmbeddedFormsDemo.Models;
using EmbeddedFormsDemo.Views;

namespace EmbeddedFormsDemo.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public User User { get; set; }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			OnSegmentChanged(segmentedControl);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			// update the native UI
			if (User != null)
			{
				welcomeLabel.Text = $"Welcome back {User.Name}!";
			}
		}

		partial void OnSegmentChanged(UISegmentedControl sender)
		{
			statusLabel.Text = $"You are on page number: {sender.SelectedSegment}";
		}

		partial void OnLoginClicked(UIButton sender)
		{
			if (!Xamarin.Forms.Forms.IsInitialized)
			{
				// initialize Xamarin.Forms before we use it
				Xamarin.Forms.Forms.Init();

				// we want to listen to the messaging center
				Xamarin.Forms.MessagingCenter.Subscribe(this, LoginPage.LoginMessage, (User user) =>
				{
					// update the app
					User = user;

					// go back to the main page
					NavigationController.PopViewController(true);
				});
			}

			// create the login page (Xamarin.Forms ContentPage)
			var loginPage = new LoginPage();

			// show the login screen
			var viewController = loginPage.CreateViewController();
			NavigationController.PushViewController(viewController, true);
		}
	}
}
