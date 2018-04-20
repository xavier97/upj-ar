using CoreGraphics;
using System.Collections.Generic;
using CoreLocation;
using Foundation;
using MapKit;
using System;
using UIKit;

namespace UPJAR
{
    public partial class MapViewController : UIViewController
    {
        MKMapView mapView;
        UISegmentedControl mapTypeSelection;
        CLLocationManager location = new CLLocationManager();
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string pinFolderPath = path + "/asset";
        private int assetKey;
        private List<CubeDetail> assetList;
        FileManager fileManager = new FileManager();


        public MapViewController(IntPtr handle) : base(handle)
        {
            Console.WriteLine(path);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            Title = "UPJ Tour";

            mapView = new MKMapView(UIScreen.MainScreen.Bounds);
            View = mapView;

            // Request permission to access device location  
            location.RequestWhenInUseAuthorization();

            // Indicates User Location  
            mapView.ShowsUserLocation = true;

            // This snippet lets you toggle between Map Types  
            int typesWidth = 260, typesHeight = 30, distanceFromBottom = 60;
            mapTypeSelection = new UISegmentedControl(new CGRect((View.Bounds.Width - typesWidth) / 2, View.Bounds.Height - distanceFromBottom, typesWidth, typesHeight));
            mapTypeSelection.InsertSegment("Standard", 0, false);
            mapTypeSelection.InsertSegment("Satellite", 1, false);
            mapTypeSelection.InsertSegment("Hybrid", 2, false);
            mapTypeSelection.SelectedSegment = 2; // Hybrid is the default selection  
            mapTypeSelection.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;

            mapTypeSelection.ValueChanged += (s, e) =>
            {
                switch (mapTypeSelection.SelectedSegment)
                {
                    case 0:
                        mapView.MapType = MKMapType.Standard;
                        break;
                    case 1:
                        mapView.MapType = MKMapType.Satellite;
                        break;
                    case 2:
                        mapView.MapType = MKMapType.Hybrid;
                        break;
                }
            };

            assetList = new List<CubeDetail>();
            assetList = fileManager.MakeAssetList();
            Console.WriteLine(assetList[0].ToString());

            mapView.Delegate = new MapDelegate(this);

            for (int i = 0; i < assetList.Count; i++){
                var latitude = Double.Parse(assetList[i].Lat);
                var longitude = Double.Parse(assetList[i].Long);
                var title = assetList[i].name;
                var desc = assetList[i].desc;
                var cubeDesc = assetList[i].descLoc;
                var cubeLoc = assetList[i].asset;

                var imageFile = pinFolderPath + "1/cubeImage0.jpg";
                UIImage image = UIImage.FromFile(imageFile);
                    
                var annotation = new BasicMapAnnotation
                    (new CLLocationCoordinate2D(latitude, longitude), title, desc, cubeDesc, image);
                mapView.AddAnnotation(annotation);

            }



            View.AddSubview(mapTypeSelection);



            if (!mapView.UserLocationVisible)
            {
                // User denied permission or device doesn't have GPS/location ability  
                // create our location and zoom to Chicago  
                CLLocationCoordinate2D coords = new CLLocationCoordinate2D(40.2675, -78.8357); // UPJ
                MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(5), MilesToLongitudeDegrees(5, coords.Latitude));

                // set the coords and zoom on the map  
                mapView.Region = new MKCoordinateRegion(coords, span);
            }
        }

        #region GPS Helpers
        public double MilesToLatitudeDegrees(double miles)
        {
            double earthRadius = 3960.0; // in miles  
            double radianToDegree = 180.0 / Math.PI;
            return (miles / earthRadius) * radianToDegree;
        }

        public double MilesToLongitudeDegrees(double miles, double atLatitude)
        {
            double earthRadius = 3960.0; // in miles
            double degreeToRadian = Math.PI / 180.0;
            double radianToDegree = 180.0 / Math.PI;

            // derive the earth's radius at that point in latitude  
            double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreeToRadian);
            return (miles / radiusAtLatitude) * radianToDegree;
        }
        #endregion

        class BasicMapAnnotation : MKAnnotation
        {
            CLLocationCoordinate2D coord;
            string title, subtitle, location;
            UIImage cubeLocation;


            public override CLLocationCoordinate2D Coordinate { get { return coord; } }
            public override void SetCoordinate(CLLocationCoordinate2D value)
            {
                coord = value;
            }
            public override string Title { get { return title; } }
            public override string Subtitle { get { return subtitle; } }

            public UIImage GetImage{ get { return cubeLocation; }}

            public string GetLocation
            {
                get
                {
                    return location;
                }
            }

            public BasicMapAnnotation(CLLocationCoordinate2D coordinate, string title, string subtitle, string location, UIImage image)
            {
                this.coord = coordinate;
                this.title = title;
                this.subtitle = subtitle;
                this.location = location;
                this.cubeLocation = image;

            }

			public override string ToString()
			{
                return string.Format(coord.ToString() + ',' + title + ',' + subtitle + ',' + location + ',' + cubeLocation);
			}
		}

        protected class MapDelegate : MKMapViewDelegate
        {
            protected string annotationIdentifier = "BasicAnnotation";
            UIButton detailButton;
            MapViewController parent;
            UIImageView image;

            public MapDelegate(MapViewController parent)
            {
                this.parent = parent;
            }


            /// <summary>
            /// This is very much like the GetCell method on the table delegate
            /// </summary>
            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                // try and dequeue the annotation view
                MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(annotationIdentifier);

                // if we couldn't dequeue one, create a new one
                if (annotationView == null)
                    annotationView = new MKPinAnnotationView(annotation as BasicMapAnnotation, annotationIdentifier);
                else // if we did dequeue one for reuse, assign the annotation to it
                    annotationView.Annotation = (BasicMapAnnotation)annotation;
                // configure our annotation view properties
                annotationView.CanShowCallout = true;
                (annotationView as MKPinAnnotationView).AnimatesDrop = true;
                (annotationView as MKPinAnnotationView).PinColor = MKPinAnnotationColor.Green;


                annotationView.Selected = true;
                // you can add an accessory view, in this case, we'll add a button on the right, and an image on the left
                detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);
                detailButton.TouchUpInside += (s, e) => {
                    Console.WriteLine("Clicked");
                    //Create Alert (can use an alert to give cube location for each given spot)-
                    var detailAlert = UIAlertController.Create("About the QR location...", (annotation as BasicMapAnnotation).GetLocation, UIAlertControllerStyle.Alert);
                    detailAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    parent.PresentViewController(detailAlert, true, null);
                };
                annotationView.RightCalloutAccessoryView = detailButton;
                annotationView.LeftCalloutAccessoryView = null;



                return annotationView;
            }

			public override void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
			{

                image = new UIImageView(new CGRect(-84, 0, 200, 200));
                image.Image = UIImage.FromFile(path + "/asset0/cubeimage0.jpg");
                Console.WriteLine(view.Annotation.ToString());
                view.AddSubview(image);

			}

			public override void DidDeselectAnnotationView(MKMapView mapView, MKAnnotationView view)
			{

                image.RemoveFromSuperview();

			}

			public override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
            {
                mapView.DidUpdateUserLocation += (sender, e) =>
                {
                    if (mapView.UserLocation != null)
                    {
                        CLLocationCoordinate2D coords = mapView.UserLocation.Coordinate;
                        MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(2), MilesToLongitudeDegrees(2, coords.Latitude));
                        mapView.Region = new MKCoordinateRegion(coords, span);
                    }
                };
            }

            public double MilesToLatitudeDegrees(double miles)
            {
                double earthRadius = 3960.0; // in miles  
                double radianToDegree = 180.0 / Math.PI;
                return (miles / earthRadius) * radianToDegree;
            }

            public double MilesToLongitudeDegrees(double miles, double atLatitude)
            {
                double earthRadius = 3960.0; // in miles
                double degreeToRadian = Math.PI / 180.0;
                double radianToDegree = 180.0 / Math.PI;

                // derive the earth's radius at that point in latitude  
                double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreeToRadian);
                return (miles / radiusAtLatitude) * radianToDegree;
            }

            // as an optimization, you should override this method to add or remove annotations as the
            // map zooms in or out.
            public override void RegionChanged(MKMapView mapView, bool animated) { }
        }


    }
}
