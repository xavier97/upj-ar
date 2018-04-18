using Foundation;
using Newtonsoft.Json;
using System;
using SystemConfiguration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace UPJAR
{
    public class FileManager
    {
        private string webserviceURL = "http://ec2-54-191-254-89.us-west-2.compute.amazonaws.com/ar-web/results.json"; // Webservice location
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string jsonPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/assets.json";
        private string newDirectory;
        private List<CubeDetail> assetList; // List of cube's assets
        private string name1;
        public FileManager()
        {
            Console.WriteLine("heloo");

            Console.WriteLine(path);

            if (assetList == null)
            {
                assetList = new List<CubeDetail>();
            }

            if (Reachability.IsHostReachable(webserviceURL)) // IF network is available...
            {
                // Checks for any changes from the webservice. If there are changes, get new data based on updated json.
                if (isChange()) // Yes.
                {
                    assetList = MakeAssetList(); // puts location of objects in memory

                    UpdateAssets(); // puts objects into storage
                }
                else
                {
                    assetList = MakeAssetList(); // puts location of objects in memory
                }

                int directoryCount = Directory.GetDirectories(path).Length;
                Console.WriteLine(directoryCount);
                Console.WriteLine(assetList.Count);
                if (directoryCount + 1 < assetList.Count) // Check if any files are missing. If yes, download them
                {
                    Console.WriteLine("get new stuff");
                    UpdateAssets();
                }
            }
        }

        /// <summary>
        /// Checks if the webservice updated with any new information. If yes, then download json as a t
        /// Text file
        /// </summary>
        /// <returns><c>true</c>, if change was made, <c>false</c> otherwise.</returns>
        private bool isChange()
        {
            if (!Directory.EnumerateFileSystemEntries(path).Any()) // Checks if there is any docs cached. if not, then create one
            {
                CacheJsonText();
                return true;
            }

            string onlineJson = WebJsonToString(); // text of web json
            string cachedJson = LocalJsonToString(); // text of cached json (from local .json file)

            if (!string.Equals(onlineJson, cachedJson)) // Checks if there is any change to the files. if not the same, then replace cache file
            {
                CacheJsonText();
                return true;
            }
            else // no changes on server were made
            {
                return false;
            }
        }

        /// <summary>
        /// Loads documents from web service into app
        /// </summary>
        private void UpdateAssets()
        {
            Console.WriteLine("update assets");

            const int ASSET_COUNT = 11; // number of assets that make up the cube/ar tour scene

            for (int member = 0; member < assetList.Count; member++)
            {
                
                // make a new directory to organize assets
                newDirectory = path + "/asset" + member.ToString();
                Directory.CreateDirectory(newDirectory);

                for (int count = 0; count < ASSET_COUNT; count++)
                {

                    DownloadFile(assetList[member].asset, assetList[member].image1, "cubeImage0.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image2, "cubeImage1.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image3, "cubeImage2.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image4, "cubeImage3.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image5, "cubeImage4.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image6, "cubeImage5.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image7, "cubeImage6.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image8, "cubeImage7.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image9, "cubeImage8.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image10, "cubeImage9.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image11, "cubeImage10.jpg", newDirectory);
                    DownloadFile(assetList[member].asset, assetList[member].image12, "cubeImage11.jpg", newDirectory);
                }

            }
        }

        /// <summary>
        /// Verifies the existence of a web resource &, if exists, downloads source.
        /// Expected result: write all web data (audio, images) to Document folder of app.
        /// Good doc: https://docs.microsoft.com/en-us/xamarin/ios/app-fundamentals/file-system/
        /// and: https://stackoverflow.com/questions/411592/how-do-i-save-a-stream-to-a-file-in-c?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
        /// </summary>
        /// <param name="url">Directory to json service</param>
        /// <param name="assetName">Name of asset in the asset list</param>
        /// <param name="imagename">Name the image should have once it's downloaded</param>
        /// <param name="location">local folder to store image</param>
        private void DownloadFile(string url, string imagename,string assetName, string location)
        {

            // i want a real url
            string replace = "/var/www/html/ar-web/assets/";
            url = url.Replace(replace, "");
            HttpWebRequest myHttpWebRequest;
            HttpWebResponse myHttpWebResponse;

         try
            {
                // Creates an HttpWebRequest for the specified URL. 
                Console.WriteLine(assetName);

                name1 = assetName;

                myHttpWebRequest  = (HttpWebRequest)WebRequest.Create("http://ec2-54-191-254-89.us-west-2.compute.amazonaws.com/ar-web/assets/" + url + assetName);

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                string method;
                method = myHttpWebResponse.Method;


                if (String.Compare(method, "GET") == 0) // IF GET METHOD SUCCESSFULLY INVKED ON SERVER, then download data
                {
                    Console.WriteLine("\nThe 'GET' method was successfully invoked on the following Web Server : {0}",
                                      myHttpWebResponse.Server);

                    // Download data:
                    // Get the stream containing content returned by the server.  
                    Stream dataStream = myHttpWebResponse.GetResponseStream();

                    // Gets a location to store file
                    Console.WriteLine(path);
                
                    Console.WriteLine(name1);
                    var newFileName = Path.Combine(location);


                    // Gets the stream containing the file for the app.
                    Console.WriteLine(newFileName);
                    Stream fileStream = File.Create(newFileName+ "/" + imagename);

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

        #region helpers

        /// <summary>
        /// Gets online json as string var
        /// </summary>
        /// <returns>json as a string.</returns>
        private string WebJsonToString()
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(webserviceURL);
            }
        }

        private string LocalJsonToString()
        {
            try
            {
                return File.ReadAllText(jsonPath);
            }
            catch (FileNotFoundException fnfe) // this should not happen irl
            {
                Console.WriteLine("\nThe following Exception was raised : {0}. " +
                                  "(Make sure that a json file was actually cached from the web!)", fnfe.Message);
                return null;
            }

        }

        /// <summary>
        /// Caches online json text as a local json file
        /// </summary>
        private void CacheJsonText()
        {
            var json = WebJsonToString();
            File.WriteAllText(jsonPath.TrimEnd(new char[] { '\r', '\n' }), json);
        }

        /// <summary>
        /// Creates list of cube asset objects
        /// </summary>
        public List<CubeDetail> MakeAssetList()
        {
            Console.WriteLine("you are here");

            List<CubeDetail> tempList = new List<CubeDetail>();

            string json = LocalJsonToString(); // Gets json that's local

            tempList = JsonConvert.DeserializeObject<List<CubeDetail>>(json); // Populate list with JSON objects



            return tempList;

        }

        #endregion

    }

}