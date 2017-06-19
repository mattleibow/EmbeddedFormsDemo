using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using EmbeddedFormsDemo.Models;

namespace EmbeddedFormsDemo.Android
{
	public class MainFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// create your fragment here
		}

		public User User => ((MainActivity)Activity).User;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.main_fragment, container, false);

			var welcome = view.FindViewById<TextView>(Resource.Id.welcome);
			var userInfo = view.FindViewById<TextView>(Resource.Id.user_info);
			if (User == null)
			{
				welcome.Text = Resources.GetString(Resource.String.welcome_unknown);
				userInfo.Visibility = ViewStates.Gone;
			}
			else
			{
				welcome.Text = string.Format(Resources.GetString(Resource.String.welcome_user), User.Name);
				userInfo.Visibility = ViewStates.Visible;
			}

			var login = view.FindViewById<Button>(Resource.Id.login);
			login.Click += OnLoginClicked;

			return view;
		}

		private void OnLoginClicked(object sender, EventArgs e)
		{
			((MainActivity)Activity).DisplayLogin();
		}
	}
}
