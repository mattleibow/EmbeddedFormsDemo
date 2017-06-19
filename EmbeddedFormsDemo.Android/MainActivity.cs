using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Xamarin.Forms.Platform.Android;

using EmbeddedFormsDemo.Models;
using EmbeddedFormsDemo.Views;
using Android.Views;

namespace EmbeddedFormsDemo.Android
{
	[Activity(
		Label = "@string/app_name",
		MainLauncher = true,
		Icon = "@drawable/icon",
		Theme = "@style/Theme.MyTheme",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// load the container layout
			SetContentView(Resource.Layout.main);

			var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.Click += OnFabClicked;

			// set the action bar
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			// create and display the main fragment (Native Android Fragment)
			var main = new MainFragment();
			FragmentManager
				.BeginTransaction()
				.Replace(Resource.Id.frameLayout, main)
				.Commit();

			// make the back button work
			FragmentManager.BackStackChanged += delegate
			{
				var hasBack = FragmentManager.BackStackEntryCount > 0;
				SupportActionBar.SetHomeButtonEnabled(hasBack);
				SupportActionBar.SetDisplayHomeAsUpEnabled(hasBack);
			};
		}

		public User User { get; private set; }

		private void OnFabClicked(object sender, EventArgs e)
		{
			const string message = "As you can probably tell, I am not that good at building native apps.";
			Snackbar.Make(FindViewById(Resource.Id.root), message, Snackbar.LengthLong)
				.SetAction("OK", delegate { })
				.Show();
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == global::Android.Resource.Id.Home && FragmentManager.BackStackEntryCount > 0)
			{
				FragmentManager.PopBackStack();
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}

		public void DisplayLogin()
		{
			if (!Xamarin.Forms.Forms.IsInitialized)
			{
				// initialize Xamarin.Forms before we use it
				Xamarin.Forms.Forms.Init(this, null);

				// we want to listen to the messaging center
				Xamarin.Forms.MessagingCenter.Subscribe(this, LoginPage.LoginMessage, (User user) =>
				{
					// update the app
					User = user;

					// go back to the main page
					FragmentManager.PopBackStack();
				});
			}

			// create the login page (Xamarin.Forms ContentPage)
			var loginPage = new LoginPage();

			// show the login screen
			var loginFragment = loginPage.CreateFragment(this);
			FragmentManager
				.BeginTransaction()
				.AddToBackStack(null)
				.Replace(Resource.Id.frameLayout, loginFragment)
				.Commit();
		}
	}
}
