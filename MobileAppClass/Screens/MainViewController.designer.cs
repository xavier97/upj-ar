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

namespace UPJARProject
{
    [Register ("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton arViewButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton mapViewButton { get; set; }

        [Action ("ArViewButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ArViewButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("MapViewButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void MapViewButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (arViewButton != null) {
                arViewButton.Dispose ();
                arViewButton = null;
            }

            if (mapViewButton != null) {
                mapViewButton.Dispose ();
                mapViewButton = null;
            }
        }
    }
}