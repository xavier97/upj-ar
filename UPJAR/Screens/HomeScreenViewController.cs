using Foundation;
using System;
using System.IO;
using System.Net;
using UIKit;

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

            /// <summary>
            /// This verifies the existence of a web resource at a specific link.
            /// </summary>
            var url = "http://ec2-34-216-11-209.us-west-2.compute.amazonaws.com/ar-web/assets/hi/incognito.jpg";
            var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
            Console.WriteLine(documents);

            try
            {
                // Creates an HttpWebRequest for the specified URL. 
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                string method;
                method = myHttpWebResponse.Method;

                if (String.Compare(method, "GET") == 0)
                {
                    Console.WriteLine("\nThe 'GET' method was successfully invoked on the following Web Server : {0}",
                                      myHttpWebResponse.Server);

                    // Download data:
                    // Get the stream containing content returned by the server.  
                    Stream dataStream = myHttpWebResponse.GetResponseStream();

                    // Gets a location to store file
                    var filename = Path.Combine(documents, "incognito.jpg");

                    // Gets the stream containing the file for the app.
                    Stream fileStream = File.Create(filename);

                    // Copy web stream to file stream
                    dataStream.CopyTo(fileStream);

                    // Close streams
                    fileStream.Close();
                    dataStream.Close();
                }

                // Releases the resources of the response.
                myHttpWebResponse.Close();
            }
            catch (WebException e)
            {
                Console.WriteLine("\nWebException raised. The following error occured : {0}", e.Status);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }

		}

		partial void MapButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to map screen");
        }

        partial void ArButton_TouchUpInside(UIButton sender)
        {
            Console.WriteLine("go to ar screen");
        }
    }
}