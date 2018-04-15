using Foundation;
using System;
using System.IO;
using System.Net;
using UIKit;
using System.Collections.Generic;

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

            // Check/Pull all new stuff from service once per app load
            FileManager fileManager = new FileManager();

        }

        //TEST
        private int assetKey()
        {
            return 1;
        }

		partial void MapButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to map screen");
        }

        partial void ArButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to ar screen");
        }

        partial void ViewQR_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to qr screen");
        }
    }
}