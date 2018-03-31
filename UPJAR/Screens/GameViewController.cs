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

        public override bool ShouldAutorotate() => true;        
     
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            sceneView = new ARSCNView()
            {
                Frame = View.Frame,
                DebugOptions = ARSCNDebugOptions.ShowFeaturePoints |
                ARSCNDebugOptions.ShowWorldOrigin,
                UserInteractionEnabled = true
            };

            //View.AddSubview(sceneView);

           
           
        }

    
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);


            var configuration = new ARWorldTrackingConfiguration
            {
                PlaneDetection = ARPlaneDetection.Horizontal,
                LightEstimationEnabled = true,

                    
            };
            configuration.PlaneDetection = ARPlaneDetection.Horizontal;
            sceneView.Scene = SCNScene.FromFile("art.scnassets/ship");
            var ship = sceneView.Scene.RootNode.FindChildNode("ship", true);
            ship.Position = new SCNVector3(0f, -2f, -9f);
            ship.Scale = new SCNVector3(1f, 1f, 1f);
           

            sceneView.Session.Run(configuration, ARSessionRunOptions.ResetTracking |
            ARSessionRunOptions.RemoveExistingAnchors);

         
            var scnView = (SCNView)View;

            scnView.Scene = SCNScene.FromFile("art.scnassets/ship");


            // allows the user to manipulate the camera
            scnView.AllowsCameraControl = true;

            // show statistics such as fps and timing information
            scnView.ShowsStatistics = true;

            // add a tap gesture recognizer
            var tapGesture = new UITapGestureRecognizer(HandleTap);

            var gestureRecognizers = new List<UIGestureRecognizer>();

            gestureRecognizers.Add(tapGesture);


            gestureRecognizers.AddRange(scnView.GestureRecognizers);
            scnView.GestureRecognizers = gestureRecognizers.ToArray();

          
            
        }
        void HandleTap(UIGestureRecognizer gestureRecognize)
        {
            Console.WriteLine("touch");
            // retrieve the SCNView
            var scnView = (SCNView)View;
          

            // check what nodes are tapped
            CGPoint p = gestureRecognize.LocationInView(scnView);
            SCNHitTestResult[] hitResults = scnView.HitTest(p, (SCNHitTestOptions)null);

            // check that we clicked on at least one object
            Console.WriteLine(hitResults.Length);
            if (hitResults.Length  > 0)
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



        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.AllButUpsideDown;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            sceneView.Session.Pause();
        }

    }
}
