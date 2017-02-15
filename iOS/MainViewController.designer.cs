// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XamMedia.iOS
{
    [Register ("ViewController")]
    partial class MainViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton YouTubeLinkButton { get; set; }

        [Action ("YouTubeLinkButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void YouTubeLinkButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (YouTubeLinkButton != null) {
                YouTubeLinkButton.Dispose ();
                YouTubeLinkButton = null;
            }
        }
    }
}