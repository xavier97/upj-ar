using Foundation;
using System;
using System.IO;
using System.Net;

namespace UPJAR
{
    public class FileManager
    {
        public FileManager()
        {
            Console.WriteLine("Constructor");
        }

        /// <summary>
        /// Downloads the file.
        /// Sent in file location (URL) and file name (name) to download file and then name file
        /// </summary>
        /// <param name="fileLocation">File location.</param>
        public void downloadFile(string url, string name)
        {
            /// <summary>
            /// This verifies the existence of a web resource at a specific link and then, if exists, downloads source.
            /// Good doc: https://docs.microsoft.com/en-us/xamarin/ios/app-fundamentals/file-system
            /// and: https://stackoverflow.com/questions/411592/how-do-i-save-a-stream-to-a-file-in-c?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
            /// </summary>
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Console.WriteLine(url); // i want a real url
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
                    var newFileName = Path.Combine(documents, name + ".jpg");

                    // Gets the stream containing the file for the app.
                    Stream fileStream = File.Create(newFileName);

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
    }
}