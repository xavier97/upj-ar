using Foundation;
using System;
using UIKit;

namespace UPJAR
{
    public partial class HomeScreenViewController : UIViewController
    {
        public HomeScreenViewController (IntPtr handle) : base (handle)
        {
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