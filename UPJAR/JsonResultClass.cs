using System;
namespace UPJAR
{
    public class JsonResultClass
    {
        public string name { get; set; }
        public string description { get; set; }
        public string assetLocation { get; set; }
        public double GPSLat { get; set; }
        public double GPSLong { get; set; }
        public string QRCode { get; set; }

        public JsonResultClass()
        {
            // add constructor stuff here.
        }

		public override string ToString()
		{
            return string.Format("{0} {1} {2} {3} {4} {5}",
                                 name, description, assetLocation, GPSLat, GPSLong, QRCode);
		}
	}
}
