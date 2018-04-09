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
        protected GameViewController(IntPtr handle) : base(handle) { 
            
        }

        private SCNMaterial[] LoadMaterials()
        {

            var a = new SCNMaterial();
            var b = new SCNMaterial();

            a.Diffuse.Contents = UIImage.FromFile("art.scnassets/texture.png");
            b.Diffuse.Contents = UIColor.Green;

            SCNMaterial[] joe = new SCNMaterial[] {b,a,a,a,b,a };
            // This demo was originally in F# :-)   
            return joe;
        }
        public async void RefreshDataAsync()
        {
            Console.WriteLine("hello");
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var result = await scanner.Scan();
            Console.WriteLine("hello");
            if (result != null)
                Console.WriteLine("REEEe");
            sceneView = new ARSCNView
            {
                Frame = View.Frame,
                DebugOptions = ARSCNDebugOptions.ShowFeaturePoints |
          ARSCNDebugOptions.ShowWorldOrigin,
                UserInteractionEnabled = true
            };

            View.AddSubview(sceneView);

            var configuration = new ARWorldTrackingConfiguration
            {
                PlaneDetection = ARPlaneDetection.Horizontal,
                LightEstimationEnabled = true
            };
           
            configuration.PlaneDetection = ARPlaneDetection.Horizontal;
            sceneView.Scene = SCNScene.FromFile("art.scnassets/cube");


            var material = new SCNMaterial();

            material.Diffuse.Contents = UIImage.FromFile("art.scnassets/texture.png");


            material.LocksAmbientWithDiffuse = true;

            


            var ship = sceneView.Scene.RootNode.FindChildNode("Cube", true);

            ship.Geometry = new SCNBox
            {
                Width = 1,
                Height = 1,
                Length = 1,
         

            };

            ship.Geometry.Materials = LoadMaterials();
            ship.Geometry.FirstMaterial.Diffuse.Contents = UIColor.Green;


            ship.Position = new SCNVector3(2f, -2f, -3f);
            ship.Scale = new SCNVector3(.5f, .5f, .5f);

           


            sceneView.Session.Run(configuration, ARSessionRunOptions.ResetTracking |
              ARSessionRunOptions.RemoveExistingAnchors);


        
            // allows the user to manipulate the camera
            //ship

            // show statistics such as fps and timing information
            sceneView.ShowsStatistics = true;






   

        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            RefreshDataAsync();

           


        }


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

           

        }





    }
}
