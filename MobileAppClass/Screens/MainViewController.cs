using System;

using UIKit;

namespace UPJARProject
{
    public partial class MainViewController : UIViewController
    {
        public MainViewController() : base("MainViewController", null)
        {
            Console.WriteLine("constructor");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        partial void MapViewButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("map button touched");

            MapViewController mapVC = new MapViewController();
            this.NavigationController.PushViewController(mapVC, true);
        }

        partial void ArViewButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("ar button touched");

            ARViewController arVC = new ARViewController();
            this.NavigationController.PushViewController(arVC, true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

