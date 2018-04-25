using Foundation;
using System;
using System.IO;
using System.Net;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;

namespace UPJAR
{
    public partial class HomeScreenViewController : UIViewController
    {
        public HomeScreenViewController (IntPtr handle) : base (handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIColor homeBackgroundColor = UIColor.FromRGB(16, 33, 63);
            UIColor navBarBackgroundColor = UIColor.FromRGB(178, 164, 108);

            HomeScreen.BackgroundColor = homeBackgroundColor;



            // Check/Pull all new stuff from service once per app load
            FileManager fileManager = new FileManager(HomeScreen);

        }

		public override void ViewDidAppear(bool animated)
		{
            base.ViewDidAppear(animated);

		}

		partial void MapButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to map screen");
        }

        partial void ArButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to ar screen");
        }

    }
}