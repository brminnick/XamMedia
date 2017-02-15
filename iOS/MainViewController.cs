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
			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{
				var options = new UIApplicationOpenUrlOptions
				{
					SourceApplication = "com.minnick.xammedia"
				};

				UIApplication.SharedApplication.OpenUrl(new NSUrl(_youtubeAppLink), options, async didYouTubeAppOpen =>
				{
					if (!didYouTubeAppOpen)
						await OpenYouTubeLinkInSFSafariViewController();
				});
			}
			else
			{
				bool didYouTubeAppOpen = false;
				didYouTubeAppOpen = UIApplication.SharedApplication.OpenUrl(new NSUrl(_youtubeAppLink));

				if (!didYouTubeAppOpen)
					await OpenYouTubeLinkInSFSafariViewController();
			}

		}

		async Task OpenYouTubeLinkInSFSafariViewController()
		{
			var youtubeLinkInSFSafariViewController = new SFSafariViewController(new NSUrl(_youtubeSafariLink));
			await PresentViewControllerAsync(youtubeLinkInSFSafariViewController, true);
		}
	}
}
