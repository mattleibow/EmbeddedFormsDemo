// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace EmbeddedFormsDemo.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl segmentedControl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel statusLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel welcomeLabel { get; set; }

        [Action ("OnSegmentChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnSegmentChanged (UIKit.UISegmentedControl sender);

        void ReleaseDesignerOutlets ()
        {
            if (segmentedControl != null) {
                segmentedControl.Dispose ();
                segmentedControl = null;
            }

            if (statusLabel != null) {
                statusLabel.Dispose ();
                statusLabel = null;
            }

            if (welcomeLabel != null) {
                welcomeLabel.Dispose ();
                welcomeLabel = null;
            }
        }
    }
}