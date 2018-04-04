// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace UPJAR
{
    [Register ("HomeScreenViewController")]
    partial class HomeScreenViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton arButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView HomeScreen { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton mapButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ViewQR { get; set; }

        [Action ("ArButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ArButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("MapButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void MapButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("ViewQR_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ViewQR_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (arButton != null) {
                arButton.Dispose ();
                arButton = null;
            }

            if (HomeScreen != null) {
                HomeScreen.Dispose ();
                HomeScreen = null;
            }

            if (mapButton != null) {
                mapButton.Dispose ();
                mapButton = null;
            }

            if (ViewQR != null) {
                ViewQR.Dispose ();
                ViewQR = null;
            }
        }
    }
}