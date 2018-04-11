using Foundation;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using UIKit;
using System.Collections.Generic;

namespace UPJAR
{
    public partial class HomeScreenViewController : UIViewController
    {
        public HomeScreenViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Check/Pull all new stuff from service once per app load
            #region Get webservice data
            // Create list of json objects
            List<JsonResultClass> assetList = new List<JsonResultClass>();

            using (var client = new WebClient()) // Get json from the web
            {
                var json = client.DownloadString("http://ec2-34-216-11-209.us-west-2.compute.amazonaws.com/ar-web/results.json"); // web service location
                assetList = JsonConvert.DeserializeObject<List<JsonResultClass>>(json); // Get the web json into memory as a list

                // Do something with the model
                Console.WriteLine(assetList[0].description); // TODO: should print "This is a test database entry" :)
            }

            FileManager fileManager = new FileManager(); // Get ready to manage file

            // Start loading everything from webservice into app
            // TODO: This should work once real URL is used.
            for (int member = 0; member < assetList.Count; member++)
            {
                fileManager.downloadFile(assetList[member].assetLocation, assetList[member].name);
            }
            #endregion

        }

		partial void MapButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to map screen");
        }

        partial void ArButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to ar screen");
        }

        partial void ViewQR_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to qr screen");
        }
    }
}