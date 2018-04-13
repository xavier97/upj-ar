using System;
using System.Dynamic;
namespace UPJAR
{
    public class CubeDetail
    {
        public string name { get; set; }
        public string description { get; set; }
        public string assetLocation { get; set; }
        public double GPSLat { get; set; }
        public double GPSLong { get; set; }
        public string QRCode { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
        public string img4 { get; set; }
        public string img5 { get; set; }
        public string img6 { get; set; }
        public string img7 { get; set; }
        public string img8 { get; set; }
        public string img9 { get; set; }
        public string img10 { get; set; }
        public string img11 { get; set; }
        public string img12 { get; set; }

		public override string ToString()
		{
            return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                                 name, description, assetLocation, GPSLat, GPSLong, QRCode,
                                 img1, img2, img3);
		}
	}
}
