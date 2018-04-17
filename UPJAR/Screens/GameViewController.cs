﻿using System;
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
        private int assetKey;
        private List<CubeDetail> assetList;

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
            string textureFolderPath = path + "/asset" + assetKey;

            Console.WriteLine(textureFolderPath);

            string[] Files = Directory.GetFiles(textureFolderPath, "*.jpg"); // Getting jpg files

            Console.WriteLine(Files[0]);

            try
            {
                a.Diffuse.Contents = UIImage.FromFile(Files[0]);
                b.Diffuse.Contents = UIImage.FromFile(Files[1]);
                c.Diffuse.Contents = UIImage.FromFile(Files[2]);
                d.Diffuse.Contents = UIImage.FromFile(Files[3]);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("\nIndexOutOfRangeException raised. The following error occured : {0}\r" +
                                  "Make sure enough images are in the specified folder (determined by key var).", e.Message);
            }

            SCNMaterial[] joe = new SCNMaterial[] { a, a, a, a, a, a };
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

            FileManager fileManager = new FileManager();
            if (assetList == null)
            {
                assetList = new List<CubeDetail>();
            }

            assetList = fileManager.MakeAssetList(); // Get asset list

            Console.WriteLine(assetList.Count);

            // if result matches location from json, pull up spec. qr; else reload qr scanner
            if (result != null)
            {
                int count = 0;
                while (count < assetList.Count)
                {
                    Console.WriteLine(count);
                    if (result.Text == assetList[count].name)
                    {
                        Console.WriteLine(result.Text);
                        assetKey = count;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not a good QR.");
                    }
                    count++;
                }
                // TODO : update AssetKey Method
            }

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
            int key = AssetKey();
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


            sceneView.Session.Run(configuration, ARSessionRunOptions.ResetTracking |
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

        #region helpers
        /// <summary>
        /// Represents the key that the QR scanner should send to let AR
        /// know what textures to use.
        /// </summary>
        /// <returns>The key.</returns>
        private int AssetKey()
        {
            return 1;
        }
        #endregion

    }
}
