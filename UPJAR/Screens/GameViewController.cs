using System;
using System.Collections.Generic;
using System.IO;
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
       
        protected GameViewController(IntPtr handle) : base(handle)
        {
            Console.WriteLine(path);
        }

        private SCNMaterial[] LoadMaterials()
        {
           
            var a = new SCNMaterial();
            var b = new SCNMaterial();
            var c = new SCNMaterial();
            var d = new SCNMaterial();

            // Determine types of texture to use, based on data sent by QR screen
            int key = assetKey();
            string textureFolderPath = path + "/asset" + key;

            string[] Files = Directory.GetFiles(textureFolderPath, "*.png"); // Getting Text files

            a.Diffuse.Contents = UIImage.FromFile(path + "/" + Files[0]);
            b.Diffuse.Contents = UIImage.FromFile(path + "/" + Files[1]);
            c.Diffuse.Contents = UIImage.FromFile(path + "/" + Files[2]);
            d.Diffuse.Contents = UIImage.FromFile(path + "/" + Files[3]);

            SCNMaterial[] joe = new SCNMaterial[] {a,a,a,a,a,a };
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
            sceneView.Scene = SCNScene.FromFile("art.scnassets/cube");
         
            var material = new SCNMaterial();

            // Determine types of texture to use, based on data sent by QR screen
            int key = assetKey();
            string textureFolderPath = path + "/asset" + key;

            string[] Files = Directory.GetFiles(textureFolderPath, "*.png"); // Getting Text files

            for (int assetCount = 0; assetCount < Files.Length; assetCount++)
            {
                material.Diffuse.Contents = UIImage.FromFile(path + "/" + Files[assetCount]);
            }


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

        /// <summary>
        /// Represents the key that the QR scanner should somehow send to let AR
        /// know what textures to use.
        /// </summary>
        /// <returns>The key.</returns>
        private int assetKey()
        {
            return 1;
        }


    }
}
