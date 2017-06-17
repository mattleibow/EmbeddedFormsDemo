using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;

namespace EmbeddedFormsDemo.Android
{
	public class MainFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.main_fragment, container, false);

			var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.Click += OnFabClicked;

			return view;
		}

		private void OnFabClicked(object sender, EventArgs e)
		{
			const string message = "As you can probably tell, I am not that good at building native apps.";
			Snackbar.Make(View, message, Snackbar.LengthLong)
				.SetAction("OK", delegate { })
				.Show();
		}
	}
}
