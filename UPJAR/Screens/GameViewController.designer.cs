// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace UPJAR
{
    [Register ("GameViewController")]
    partial class GameViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        SceneKit.SCNView ARScreen { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ARScreen != null) {
                ARScreen.Dispose ();
                ARScreen = null;
            }
        }
    }
}