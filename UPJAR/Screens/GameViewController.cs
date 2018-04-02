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



        protected GameViewController(IntPtr handle) : base(handle) { }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override bool ShouldAutorotate() => true;        
     
        public override void ViewDidLoad()
        {
            var configuration = new ARWorldTrackingConfiguration
            {
                PlaneDetection = ARPlaneDetection.Horizontal,
                LightEstimationEnabled = true,


            };
            configuration.PlaneDetection = ARPlaneDetection.Horizontal;
            //sceneView.Scene = SCNScene.FromFile("art.scnassets/ship");
            //var ship = sceneView.Scene.RootNode.FindChildNode("ship", true);
            //ship.Position = new SCNVector3(0f, -2f, -9f);
            //ship.Scale = new SCNVector3(1f, 1f, 1f);


            //sceneView.Session.Run(configuration, ARSessionRunOptions.ResetTracking |
            //ARSessionRunOptions.RemoveExistingAnchors);


            var scnView = (SCNView)View;

            scnView.Scene = SCNScene.FromFile("art.scnassets/cube");


            var ship = scnView.Scene.RootNode.FindChildNode("Cube", true);
            ship.Position = new SCNVector3(0f, 0f,0f);
            ship.Scale = new SCNVector3(1f, 1f, 1f);

            // create and add a light to the scene
            var lightNode = SCNNode.Create();
            lightNode.Light = SCNLight.Create();
            lightNode.Light.LightType = SCNLightType.Omni;
            lightNode.Position = new SCNVector3(0, 10, 10);
            scnView.Scene.RootNode.AddChildNode(lightNode);

            // create and add an ambient light to the scene
            var ambientLightNode = SCNNode.Create();
            ambientLightNode.Light = SCNLight.Create();
            ambientLightNode.Light.LightType = SCNLightType.Ambient;
            ambientLightNode.Light.Color = UIColor.DarkGray;
            scnView.Scene.RootNode.AddChildNode(ambientLightNode);
         


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
            base.ViewDidLoad();

        }

    
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
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
            //if (hitResults.Length  > 0)
            //{
            //    // retrieved the first clicked object
            //    SCNHitTestResult result = hitResults[0];

            //    // get its material
            //    SCNMaterial material = result.Node.Geometry.FirstMaterial;

            //    // highlight it
            //    SCNTransaction.Begin();
            //    SCNTransaction.AnimationDuration = 0.5f;

            //    // on completion - unhighlight
            //    SCNTransaction.SetCompletionBlock(() =>
            //    {
            //        SCNTransaction.Begin();
            //        SCNTransaction.AnimationDuration = 0.5f;



            //        SCNTransaction.Commit();
            //    });

            //    material.Emission.Contents = UIColor.Red;

            //    SCNTransaction.Commit();
            //}
        }



        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.AllButUpsideDown;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

           
        }

    }
}
