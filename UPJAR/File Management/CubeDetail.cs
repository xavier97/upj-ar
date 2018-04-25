using System;
using System.Dynamic;
namespace UPJAR
{
    public class CubeDetail
    {
        public string name { get; set; }
        public string desc { get; set; }
        public string descLoc { get; set; }
        public string asset { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string timestamp { get; set; }
        public string image1 { get; set; }
        public string image2 { get; set; }
        public string image3 { get; set; }
        public string image4 { get; set; }
        public string image5 { get; set; }
        public string image6 { get; set; }
        public string image7 { get; set; }
        public string image8 { get; set; }
        public string image9 { get; set; }
        public string image10 { get; set; }
        public string image11 { get; set; }
        public string image12 { get; set; }
        public string audio { get; set; }
        public string link { get; set; }

        public override string ToString()
		{
            return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18}",
                                 name, desc, descLoc, asset, Lat, Long, timestamp,
                                 image1, image2, image3, image4, image5, image6, image7, image8, image9, image10, image11, image12, audio);
		}
	}
}
