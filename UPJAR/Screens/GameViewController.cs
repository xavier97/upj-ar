using System;
using System.Collections.Generic;
using ARKit;
using CoreGraphics;
using SceneKit;
using UIKit;

namespace UPJAR
{
    public partial class GameViewController : UIViewController
    {
        private ARSCNView sceneView;
        protected GameViewController(IntPtr handle) : base(handle) { }



        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            sceneView = new ARSCNView
            {
                Frame = View.Frame,
                DebugOptions = ARSCNDebugOptions.ShowFeaturePoints |
          ARSCNDebugOptions.ShowWorldOrigin,
                UserInteractionEnabled = true
            };

            View.AddSubview(sceneView);
        }


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var configuration = new ARWorldTrackingConfiguration
            {
                PlaneDetection = ARPlaneDetection.Horizontal,
                LightEstimationEnabled = true
            };
           
            configuration.PlaneDetection = ARPlaneDetection.Horizontal;
            sceneView.Scene = SCNScene.FromFile("art.scnassets/cube");
            var ship = sceneView.Scene.RootNode.FindChildNode("Cube", true);
            ship.Position = new SCNVector3(0f, 1f, 3f);
            ship.Scale = new SCNVector3(.5f, .5f, .5f);

            sceneView.Session.Run(configuration, ARSessionRunOptions.ResetTracking |
              ARSessionRunOptions.RemoveExistingAnchors);


        
            // allows the user to manipulate the camera
            //ship

            // show statistics such as fps and timing information
            sceneView.ShowsStatistics = true;

            // add a tap gesture recognizer
            var tapGesture = new UITapGestureRecognizer(HandleTap);


            var gestureRecognizers = new List<UIGestureRecognizer>();




            gestureRecognizers.Add(tapGesture);


            gestureRecognizers.AddRange(sceneView.GestureRecognizers);
            sceneView.GestureRecognizers = gestureRecognizers.ToArray();

          
            

     

        }
        void HandleTap(UIGestureRecognizer gestureRecognize)
        {
            Console.WriteLine("touch");
            // retrieve the SCNView
          


            // check what nodes are tapped
            CGPoint p = gestureRecognize.LocationInView(sceneView);
            SCNHitTestResult[] hitResults = sceneView.HitTest(p, (SCNHitTestOptions)null);

            // check that we clicked on at least one object
            Console.WriteLine(hitResults.Length);
            if (hitResults.Length > 0)
            {
                // retrieved the first clicked object
                SCNHitTestResult result = hitResults[0];

                // get its material
                SCNMaterial material = result.Node.Geometry.FirstMaterial;

                // highlight it
                SCNTransaction.Begin();
                SCNTransaction.AnimationDuration = 0.5f;

                // on completion - unhighlight
                SCNTransaction.SetCompletionBlock(() =>
                {
                    SCNTransaction.Begin();
                    SCNTransaction.AnimationDuration = 0.5f;



                    SCNTransaction.Commit();
                });

                material.Emission.Contents = UIColor.Red;

                SCNTransaction.Commit();
            }
        }


        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            sceneView.Session.Pause();
        }

    }
}
