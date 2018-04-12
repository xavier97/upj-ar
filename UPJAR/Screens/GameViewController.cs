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
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
       
        protected GameViewController(IntPtr handle) : base(handle) { 
            
        }

        private SCNMaterial[] LoadMaterials()
        {
           
            var a = new SCNMaterial();
            var b = new SCNMaterial();

            a.Diffuse.Contents = UIImage.FromFile(path + "/texture.png");
            b.Diffuse.Contents = UIColor.Green;

            SCNMaterial[] joe = new SCNMaterial[] {b,b,b,b,b,b };
            // This demo was originally in F# :-)   
            return joe;
        }
        private SCNMaterial[] LoadMaterials2()
        {

            var z = new SCNMaterial();
            var y = new SCNMaterial();

            z.Diffuse.Contents = UIImage.FromFile(path + "/texture.png");
            y.Diffuse.Contents = UIColor.Red;

            SCNMaterial[] red = new SCNMaterial[] { y, y, y, y, y, y };
            // This demo was originally in F# :-)   
            return red;
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
            sceneView.Scene = SCNScene.FromFile(path + "/cube");
         
            var material = new SCNMaterial();

            material.Diffuse.Contents = UIImage.FromFile(path + "/texture.png");


            material.LocksAmbientWithDiffuse = true;


            var ship = sceneView.Scene.RootNode.FindChildNode("Cube", true);
           

            ship.Geometry = new SCNBox
            {
                Width = 1,
                Height = 1,
                Length = 1,
         

            };

           
            ship.Geometry.Materials = LoadMaterials();


            ship.Position = new SCNVector3(1f, 0f, 2f);
            ship.Scale = new SCNVector3(.4f, .4f, .4f);


            var ship2 = ship.Clone();

             ship2.Geometry = new SCNBox
            {
                Width = 1,
                Height = 1,
                Length = 1,


            };
            ship2.Geometry.Materials = LoadMaterials2();
          

            ship2.Position = new SCNVector3(-1f, 0f, 2f);
            ship2.Scale = new SCNVector3(.4f, .4f, .4f);

            sceneView.Scene.RootNode.Add(ship2);


            sceneView.Session.Run(configuration,ARSessionRunOptions.ResetTracking|
              ARSessionRunOptions.RemoveExistingAnchors);

        

        


        
            // allows the user to manipulate the camera
           

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
