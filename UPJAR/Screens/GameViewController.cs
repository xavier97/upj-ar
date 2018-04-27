using System;
using System.Collections.Generic;
using System.IO;
using ARKit;
using CoreGraphics;
using SceneKit;
using UIKit;
using Foundation;
using AVFoundation;
using SafariServices;
using SpriteKit;

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
        public string siteURL;
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

            SCNMaterial[] joe = new SCNMaterial[] { a, b, c, d, placeholder, placeholder };
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

            SCNMaterial[] joe = new SCNMaterial[] { a, b, c, d, placeholder, placeholder };
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

            SCNMaterial[] joe = new SCNMaterial[] { a, b, c, d, placeholder, placeholder };
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
                        siteURL = assetList[count].link;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not a good QR.");
                    }
                    count++;
                }

                if (assetKey == -1)
                {
                    assetKey = -2;
                }

            }
            Console.WriteLine(assetKey);
            if (assetKey == -1)
            {


                NavigationController.PopViewController(true);




            }
            else if (assetKey == -2)
            {

                var alert1 = UIAlertController.Create("QR Invalid", "The QR Code you scanned is invalid please use QR codes located at tour locations", UIAlertControllerStyle.Alert);
                alert1.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, alert => NavigationController.PopViewController(true)));
                PresentViewController(alert1, true, null);
            }
            else
            {


                //generates the sceneview 
                sceneView = new ARSCNView
                {
                    Frame = View.Frame,
                    //DebugOptions = ARSCNDebugOptions.ShowFeaturePoints |
                    //ARSCNDebugOptions.ShowWorldOrigin,
                    UserInteractionEnabled = true
                };

               

                // TODO: TEST THIS
                AddFooter(); // Add footer with buttons

                var configuration = new ARWorldTrackingConfiguration
                {
                    
                    PlaneDetection = ARPlaneDetection.None
                };



                sceneView.Scene = new SCNScene();


                sceneView.Session.Run(configuration, ARSessionRunOptions.RemoveExistingAnchors);

                View.AddSubview(sceneView);

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
                    Width = 2,
                    Height = 2,
                    Length = 2,
                };


                ship.Geometry.Materials = LoadMaterials();


                ship.Position = new SCNVector3(0f, .1f, 6f);
                //ship.Scale = new SCNVector3(1f, 1f, 1f);

                sceneView.Scene.RootNode.AddChildNode(ship);



                var ship2 = ship.Clone();

                ship2.Geometry = new SCNBox
                {
                    Width = 2,
                    Height = 2,
                    Length = 2,


                };

                var ship3 = ship.Clone();

                ship3.Geometry = new SCNBox
                {
                    Width = 2,
                    Height = 2,
                    Length = 2,


                };
                //clones the ships to make models of the other scenes
                var ship4 = ship.Clone();

                ship4.Geometry = new SCNPlane
                {
                    Width = 2,
                    Height = 2
                };

                //var panel = ship.Clone();
                //panel.Geometry = new SCNPlane
                //{
                //    Width = 1,
                //    Height = 1
                //};


                //var label = new SKLabelNode();

                //label.Text = assetList[assetKey].desc;



                //var color2 = new SCNMaterial();

                //color2.Diffuse.Contents = label;
                //color2.DoubleSided = true;

                //SCNMaterial[] color3 = new SCNMaterial[] { color2 };

                //panel.Geometry.Materials = color3;


                //Mats for the ship4 which is the welcome screen of our tour

                var color = new SCNMaterial();

                var info = new SCNMaterial();

                info.Diffuse.Contents = UIImage.FromFile("art.scnassets/info.jpg");

                SCNMaterial[] infos = new SCNMaterial[] { info };

                info.DoubleSided = true;
                var instruction1 = ship.Clone();

                var instruction2 = ship.Clone();

                var instruction3 = ship.Clone();


                instruction1.Geometry = new SCNPlane
                {
                    Width = 2,
                    Height = 2
                };

                instruction2.Geometry = new SCNPlane
                {
                    Width = 2,
                    Height = 2
                };


                instruction3.Geometry = new SCNPlane
                {
                    Width = 2,
                    Height = 2
                };



                instruction1.Geometry.Materials = infos;

                instruction2.Geometry.Materials = infos;

                instruction3.Geometry.Materials = infos;



                color.Diffuse.Contents = UIImage.FromFile("art.scnassets/university-of-pittsburgh-at-johnstown_2015-11-04_15-39-42.591-1.jpg");

                SCNMaterial[] colors = new SCNMaterial[] { color };

                ship4.Geometry.Materials = colors;

                ship2.Geometry.Materials = LoadMaterials2();

                sceneView.Scene.RootNode.AddChildNode(ship2);

                sceneView.Scene.RootNode.AddChildNode(instruction1);

                sceneView.Scene.RootNode.AddChildNode(instruction3);


                instruction1.Position = new SCNVector3(4f, 3f, 3f);


                ship2.Position = new SCNVector3(4f, .1f, 3f);



                ship3.Geometry.Materials =  LoadMaterials3();

                sceneView.Scene.RootNode.AddChildNode(instruction2);

                sceneView.Scene.RootNode.AddChildNode(ship3);


                instruction2.Position = new SCNVector3(-4f, 4f, 3f);

                instruction3.Position = new SCNVector3(0f, 4f, 6f);

                ship3.Position =  new  SCNVector3(-4f,  .1f, 3f);

                sceneView.Scene.RootNode.AddChildNode(ship4);

                ship4.Position = new SCNVector3(0f, -.5f, -3.5f);



                //sceneView.Scene.RootNode.AddChildNode(panel);

                //panel.Position = new SCNVector3(0f, .5f, 2f);




            }
        }

        /// <summary>
        /// Adds footer to view.
        /// </summary>
        private void AddFooter()
        {

            UIToolbar toolbar = new UIToolbar();
            toolbar.BarStyle = UIBarStyle.BlackOpaque;

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
                , new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) {  Width = 20 }
                , new UIBarButtonItem(UIBarButtonSystemItem.Search, (s,e) =>{


                    var url = new NSUrl(siteURL);
                    var sfViewController = new SFSafariViewController(url);

                    PresentViewController(sfViewController, true, null);


                })
                , new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) {  Width = 20 }
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
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

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
