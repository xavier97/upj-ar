using System;
using System.Collections.Generic;
using System.IO;
using ARKit;
using CoreGraphics;
using SceneKit;
using UIKit;
using Foundation;
using AVFoundation;


namespace UPJAR
{
    public partial class GameViewController : UIViewController
    {
        private ARSCNView sceneView;
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private int assetKey = -1;
        private List<CubeDetail> assetList;
        public AVAudioPlayer thhing;
        public bool displayed = false;
        public NSUrl url;
        public string descriptText;
        DescriptionOverlay loadPop; // ref to overlay control
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
            var placeholder = new SCNMaterial();

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
                placeholder.Diffuse.Contents = UIImage.FromFile("art.scnassets/download.jpg");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("\nIndexOutOfRangeException raised. The following error occured : {0}\r" +
                                  "Make sure enough images are in the specified folder (determined by key var).", e.Message);
            }

            SCNMaterial[] joe = new SCNMaterial[] { a, b,  c,d,placeholder, placeholder};
            // This demo was originally in F# :-)   
            return joe;
        }
       
        private SCNMaterial[] LoadMaterials2()
        {

            var a = new SCNMaterial();
            var b = new SCNMaterial();
            var c = new SCNMaterial();
            var d = new SCNMaterial();
            var placeholder = new SCNMaterial();

            // Determine types of texture to use, based on data sent by QR screen
            string textureFolderPath = path + "/asset" + assetKey;

            Console.WriteLine(textureFolderPath);

            string[] Files = Directory.GetFiles(textureFolderPath, "*.jpg"); // Getting jpg files

            Console.WriteLine(Files[0]);

            try
            {
                a.Diffuse.Contents = UIImage.FromFile(Files[4]);
                b.Diffuse.Contents = UIImage.FromFile(Files[5]);
                c.Diffuse.Contents = UIImage.FromFile(Files[6]);
                d.Diffuse.Contents = UIImage.FromFile(Files[7]);
                placeholder.Diffuse.Contents = UIImage.FromFile("art.scnassets/download.jpg");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("\nIndexOutOfRangeException raised. The following error occured : {0}\r" +
                                  "Make sure enough images are in the specified folder (determined by key var).", e.Message);
            }

            SCNMaterial[] joe = new SCNMaterial[] {  a, b,  c, d,placeholder, placeholder };
            // This demo was originally in F# :-)   
            return joe;
        }
        private SCNMaterial[] LoadMaterials3()
        {

            var a = new SCNMaterial();
            var b = new SCNMaterial();
            var c = new SCNMaterial();
            var d = new SCNMaterial();
            var placeholder = new SCNMaterial();

            // Determine types of texture to use, based on data sent by QR screen
            string textureFolderPath = path + "/asset" + assetKey;

            Console.WriteLine(textureFolderPath);

            string[] Files = Directory.GetFiles(textureFolderPath, "*.jpg"); // Getting jpg files

            Console.WriteLine(Files[0]);

            try
            {
                a.Diffuse.Contents = UIImage.FromFile(Files[8]);
                b.Diffuse.Contents = UIImage.FromFile(Files[9]);
                c.Diffuse.Contents = UIImage.FromFile(Files[10]);
                d.Diffuse.Contents = UIImage.FromFile(Files[11]);
                placeholder.Diffuse.Contents = UIImage.FromFile("art.scnassets/download.jpg");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("\nIndexOutOfRangeException raised. The following error occured : {0}\r" +
                                  "Make sure enough images are in the specified folder (determined by key var).", e.Message);
            }

            SCNMaterial[] joe = new SCNMaterial[] {   a,b,  c, d, placeholder, placeholder};
            // This demo was originally in F# :-)   
            return joe;
        }

        public async void RefreshDataAsync()
        {
            Console.WriteLine("hello");
            FileManager fileManager = new FileManager(sceneView);
            if (assetList == null)
            {
                assetList = new List<CubeDetail>();
            }

            assetList = fileManager.MakeAssetList(); // Get asset list

            Console.WriteLine(assetList.Count);

            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var result = await scanner.Scan();
           
            // if result matches location from json, pull up spec. qr; else reload qr scanner
            if (result != null)
            {
                int count = 0;
                while (count < assetList.Count)
                {
                    Console.WriteLine(count);
                  

                    if (result.Text == assetList[count].name)
                    {
                       

                        assetKey = count;
                        Title = assetList[count].name;
                        descriptText = assetList[count].desc;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not a good QR.");
                    }
                    count++;
                }

                if(assetKey == -1){
                    assetKey = -2;
                }

            }
            Console.WriteLine(assetKey);
            if (assetKey == -1)
            {
                
                    
                 NavigationController.PopViewController(true);
               


               
            }
            else if(assetKey == -2){
                
                var alert1 = UIAlertController.Create("QR Invalid", "The QR Code you scanned is invalid please use QR codes located at tour locations", UIAlertControllerStyle.Alert);
                alert1.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, alert => NavigationController.PopViewController(true)));
                PresentViewController(alert1, true, null);
            }
            else{
                
           
            sceneView = new ARSCNView
            {
                Frame = View.Frame,
                DebugOptions = ARSCNDebugOptions.ShowFeaturePoints |
                ARSCNDebugOptions.ShowWorldOrigin,
                UserInteractionEnabled = true
            };

            View.AddSubview(sceneView);

            // TODO: TEST THIS
            AddFooter(); // Add footer with buttons

                var configuration = new ARWorldTrackingConfiguration
                {
                    PlaneDetection  = ARPlaneDetection.Horizontal,
                    LightEstimationEnabled = true
            };

            configuration.PlaneDetection = ARPlaneDetection.Horizontal;
            sceneView.Scene = new SCNScene();

            var material = new SCNMaterial();

            // Determine types of texture to use, based on data sent by QR screen
            int key = assetKey;
            string textureFolderPath = path + "/asset" + key;

            string[] Files = Directory.GetFiles(textureFolderPath, "*.png"); // Getting Text files

            for (int assetCount = 0; assetCount < Files.Length; assetCount++)
            {
                material.Diffuse.Contents = UIImage.FromFile(path + "/" + Files[assetCount]);
            }


            material.LocksAmbientWithDiffuse = true;


            var ship = new SCNNode();


            ship.Geometry = new SCNBox
            {
                Width = 1,
                Height = 1,
                Length = 1,
            };


            ship.Geometry.Materials = LoadMaterials();


            ship.Position = new SCNVector3(1f, 0f, 0f);
            ship.Scale = new SCNVector3(.5f, .5f, .5f);

            sceneView.Scene.RootNode.AddChildNode(ship);



            var ship2 = ship.Clone();

            ship2.Geometry = new SCNBox
            {
                Width = 1,
                Height = 1,
                Length = 1,


            };
            
                var ship3 = ship.Clone();

            ship3.Geometry = new SCNBox
            {
                Width = 1,
                Height = 1,
                Length = 1,


            };

            ship2.Geometry.Materials = LoadMaterials2();

            sceneView.Scene.RootNode.AddChildNode(ship2);
            
            ship2.Position = new SCNVector3(-1f, 0f, 1f);

            ship3.Geometry.Materials = LoadMaterials3();

            sceneView.Scene.RootNode.AddChildNode(ship3);

            ship3.Position = new SCNVector3(-3f, 0f, 0f);
      
            sceneView.Session.Run(configuration, ARSessionRunOptions.ResetTracking );
            
            }
        }

        /// <summary>
        /// Adds footer to view.
        /// </summary>
        private void AddFooter()
        {
           

            string textureFolderPath = path + "/asset" + assetKey;
            string[] Files = Directory.GetFiles(textureFolderPath, "*.mp3");

            url = NSUrl.FromFilename(Files[0]);
            thhing = AVAudioPlayer.FromUrl(url);
           

            // Make footer buttons here.
            this.SetToolbarItems(new UIBarButtonItem[] {
                new UIBarButtonItem(UIBarButtonSystemItem.Play, (s,e) =>
                {

                   
                     if (thhing.Playing != true)
                    {
                        thhing.Play();
                    }
                    else
                    {
                        thhing.Stop();
                    }

                })
                , new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) {  Width = 50 }
                , new UIBarButtonItem(UIBarButtonSystemItem.Bookmarks, (s,e) => {


                    if(displayed == false){
                        displayed = true;
                        var bounds = UIScreen.MainScreen.Bounds;
                        loadPop = new DescriptionOverlay(bounds, descriptText);
                        View.InvokeOnMainThread(() =>
                        {
                            // show the loading overlay on the UI thread using the correct orientation sizing
                            View.Add(loadPop);
                        });
                    }
                    else{
                        displayed = false;
                        View.InvokeOnMainThread(() =>
                        {
                        loadPop.Hide();
                         });                   
                    }
                  
                        })
                    }, false);

            this.NavigationController.ToolbarHidden = false;
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

		public override void ViewWillDisappear(bool animated)
		{

            //Turns off the sound playing if the view disapears
          
            if (thhing != null)
            {
                if (thhing.Playing == true)
                {
                    thhing.Stop();
                }
            }
			base.ViewWillDisappear(animated);
		}
		public override void ViewWillAppear(bool animated)
        {
            
            base.ViewWillAppear(animated);
           
        }
    }
}
