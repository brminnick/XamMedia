using System;
using Foundation;
using UIKit;
using SafariServices;
using System.Threading.Tasks;

namespace XamMedia.iOS
{
	public partial class MainViewController : UIViewController
	{
		#region Constructors
		public MainViewController(IntPtr handle) : base(handle)
		{
		}
		#endregion

		#region Enums
		enum AppType { YouTube, Vimeo };
		#endregion

		#region Methods
		async partial void VimeoLinkButton_TouchUpInside(UIButton sender)
		{
			var canOpenVimeoApp = UIApplication.SharedApplication.CanOpenUrl(GetAppUrl(AppType.Vimeo));

			if (canOpenVimeoApp)
				OpenLinkInApp(AppType.Vimeo);
			else
				await OpenLinkInSFSafariViewController(@"https://vimeo.com/7913667");
		}

		async partial void YouTubeLinkButton_TouchUpInside(UIButton sender)
		{
			var canOpenYouTubeApp = UIApplication.SharedApplication.CanOpenUrl(GetAppUrl(AppType.YouTube));

			if (canOpenYouTubeApp)
				OpenLinkInApp(AppType.YouTube);
			else
				await OpenLinkInSFSafariViewController(@"https://www.youtube.com/watch?v=JJB5ankU9GA");

		}

		void OpenLinkInApp(AppType appType)
		{
			var appUrl = GetAppUrl(appType);

			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{
				var options = new UIApplicationOpenUrlOptions
				{
					SourceApplication = "com.minnick.xammedia"
				};


				UIApplication.SharedApplication.OpenUrl(appUrl, options, null);
			}
			else
			{
				UIApplication.SharedApplication.OpenUrl(appUrl);
			}
		}

		async Task OpenLinkInSFSafariViewController(string url)
		{
			var youtubeLinkInSFSafariViewController = new SFSafariViewController(new NSUrl(url));
			await PresentViewControllerAsync(youtubeLinkInSFSafariViewController, true);
		}

		NSUrl GetAppUrl(AppType appType)
		{
			switch (appType)
			{
				case AppType.Vimeo:
					return new NSUrl(@"vimeo://app.vimeo.com/videos/7913667");
				case AppType.YouTube:
					return new NSUrl(@"youtube://JJB5ankU9GA");
				default:
					throw new Exception("App Type Not Supported");
			}
		}
		#endregion
	}
}
