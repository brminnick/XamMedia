using System;
using System.Threading.Tasks;

using UIKit;
using Foundation;
using SafariServices;

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
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			YouTubeLinkButton.AccessibilityIdentifier = AutomationConstants.YouTubeButton;
			VimeoLinkButton.AccessibilityIdentifier = AutomationConstants.VimeoButton;
		}

		async partial void VimeoLinkButton_TouchUpInside(UIButton sender)
		{
			AnalyticsHelpers.TrackEvent(AnalyticsConstants.VimeoButtonTapped);

			var canOpenVimeoApp = UIApplication.SharedApplication.CanOpenUrl(GetAppUrl(AppType.Vimeo));

			if (canOpenVimeoApp)
				OpenLinkInApp(AppType.Vimeo);
			else
				await OpenLinkInSFSafariViewController(GetWebUrl(AppType.Vimeo));
		}

		async partial void YouTubeLinkButton_TouchUpInside(UIButton sender)
		{
			AnalyticsHelpers.TrackEvent(AnalyticsConstants.YouTubeButtonTapped);

			var canOpenYouTubeApp = UIApplication.SharedApplication.CanOpenUrl(GetAppUrl(AppType.YouTube));

			if (canOpenYouTubeApp)
				OpenLinkInApp(AppType.YouTube);
			else
				await OpenLinkInSFSafariViewController(GetWebUrl(AppType.YouTube));

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

		async Task OpenLinkInSFSafariViewController(NSUrl url)
		{
			var linkInSFSafariViewController = new SFSafariViewController(url);

			await PresentViewControllerAsync(linkInSFSafariViewController, true);
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

		NSUrl GetWebUrl(AppType appType)
		{
			switch (appType)
			{
				case AppType.Vimeo:
					return new NSUrl(@"https://vimeo.com/7913667");
				case AppType.YouTube:
					return new NSUrl((@"https://www.youtube.com/watch?v=JJB5ankU9GA");
				default:
					throw new Exception("App Type Not Supported");
			}
		}
		#endregion
	}
}
