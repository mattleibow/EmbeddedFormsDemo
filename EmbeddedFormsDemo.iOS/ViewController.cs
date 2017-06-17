using System;
using UIKit;

using EmbeddedFormsDemo.Models;

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

			welcomeLabel.Text = $"Welcome back {User.Name}!";

			OnSegmentChanged(segmentedControl);
		}

		partial void OnSegmentChanged(UISegmentedControl sender)
		{
			statusLabel.Text = $"You are on page number: {sender.SelectedSegment}";
		}
	}
}
