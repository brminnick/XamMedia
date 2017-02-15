using System;
using Foundation;
using UIKit;
using SafariServices;
using System.Threading.Tasks;

namespace XamMedia.iOS
{
	public partial class MainViewController : UIViewController
	{
		string _youtubeSafariLink = @"https://www.youtube.com/watch?v=JJB5ankU9GA";
		string _youtubeAppLink = @"youtube://JJB5ankU9GA";

		public MainViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			YouTubeLinkButton.AccessibilityIdentifier = "myButton";
		}

		async partial void YouTubeLinkButton_TouchUpInside(UIKit.UIButton sender)
		{
			var canOpenYouTubeApp = UIApplication.SharedApplication.CanOpenUrl(new NSUrl(_youtubeAppLink));

			if (canOpenYouTubeApp)
			{
				OpenYouTubeLinkInApp();
			}
			else
			{
				await OpenYouTubeLinkInSFSafariViewController();
			}

		}

		void OpenYouTubeLinkInApp()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{
				var options = new UIApplicationOpenUrlOptions
				{
					SourceApplication = "com.minnick.xammedia"
				};

				UIApplication.SharedApplication.OpenUrl(new NSUrl(_youtubeAppLink), options, null);
			}
			else
			{
				UIApplication.SharedApplication.OpenUrl(new NSUrl(_youtubeAppLink));
			}
		}

		async Task OpenYouTubeLinkInSFSafariViewController()
		{
			var youtubeLinkInSFSafariViewController = new SFSafariViewController(new NSUrl(_youtubeSafariLink));
			await PresentViewControllerAsync(youtubeLinkInSFSafariViewController, true);
		}
	}
}
