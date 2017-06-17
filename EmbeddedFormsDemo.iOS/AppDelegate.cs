using Foundation;
using UIKit;
using Xamarin.Forms;

using EmbeddedFormsDemo.Views;

namespace EmbeddedFormsDemo.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			// initialize Xamarin.Forms so we can use it later
			Xamarin.Forms.Forms.Init();

			// create the login page (Xamarin.Forms ContentPage)
			var loginPage = new LoginPage();

			// attach an event to navigate to the main view controller after logging in
			loginPage.LoggedIn += user =>
			{
				// create the main view controller (Native iOS UIViewController)
				var storyboard = UIStoryboard.FromName("Main", null);
				var main = storyboard.InstantiateInitialViewController() as ViewController;

				main.User = user;

				// replace the login with the main view controller
				Window.RootViewController = main;
				UIView.Transition(Window, 0.5, UIViewAnimationOptions.TransitionFlipFromRight, () => Window.RootViewController = main, null);
			};

			// show the login screen
			Window.RootViewController = loginPage.CreateViewController();

			return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}
}
