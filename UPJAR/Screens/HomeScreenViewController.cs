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

        }

		public override void ViewDidAppear(bool animated)
		{
            base.ViewDidAppear(animated);

            // Check/Pull all new stuff from service once per app load
            FileManager fileManager = new FileManager();
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