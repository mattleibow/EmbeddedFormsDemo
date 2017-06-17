using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Xamarin.Forms.Platform.Android;

using EmbeddedFormsDemo.Views;

namespace EmbeddedFormsDemo.Android
{
	[Activity(
		Label = "@string/app_name",
		MainLauncher = true,
		Icon = "@drawable/icon",
		Theme = "@style/Theme.AppCompat.Light.DarkActionBar",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// load the container layout
			SetContentView(Resource.Layout.main);

			// make the back button work
			FragmentManager.BackStackChanged += delegate
			{
				var hasBack = FragmentManager.BackStackEntryCount > 0;
				ActionBar.SetHomeButtonEnabled(hasBack);
				ActionBar.SetDisplayHomeAsUpEnabled(hasBack);
			};

			// initialize Xamarin.Forms so we can use it later
			Xamarin.Forms.Forms.Init(this, null);

			// create the login page (Xamarin.Forms ContentPage)
			var loginPage = new LoginPage();

			// attach an event to navigate to the main fragment after logging in
			loginPage.LoggedIn += user =>
			{
				// create the main fragment (Native Android Fragment)
				var main = new MainFragment();

				// replace the login with the main fragment, without a back stack
				FragmentManager
					.BeginTransaction()
					.Replace(Resource.Id.frameLayout, main)
					.Commit();

				// show some message for some random reason
				Toast.MakeText(this, $"Welcome back {user.Name}!", ToastLength.Long).Show();
			};

			// show the login screen
			var loginFragment = loginPage.CreateFragment(this);
			FragmentManager
				.BeginTransaction()
				.Replace(Resource.Id.frameLayout, loginFragment)
				.Commit();
		}
	}
}
