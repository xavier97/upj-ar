using CoreGraphics;
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

        public MapViewController (IntPtr handle) : base (handle)
        {
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

            mapView.Delegate = new MapDelegate(this);

            var annotation = new BasicMapAnnotation
                (new CLLocationCoordinate2D(40.2675, -78.8357), "UPJ", "Blackington Hall");
            mapView.AddAnnotation(annotation);

            View.AddSubview(mapTypeSelection);



            if (!mapView.UserLocationVisible)
            {
                // User denied permission or device doesn't have GPS/location ability  
                // create our location and zoom to Chicago  
                CLLocationCoordinate2D coords = new CLLocationCoordinate2D(40.2675, -78.8357); // UPJ
                MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(20), MilesToLongitudeDegrees(20, coords.Latitude));

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
            string title, subtitle;

            public override CLLocationCoordinate2D Coordinate { get { return coord; } }
            public override void SetCoordinate(CLLocationCoordinate2D value)
            {
                coord = value;
            }
            public override string Title { get { return title; } }
            public override string Subtitle { get { return subtitle; } }
            public BasicMapAnnotation(CLLocationCoordinate2D coordinate, string title, string subtitle)
            {
                this.coord = coordinate;
                this.title = title;
                this.subtitle = subtitle;
            }
        }

        protected class MapDelegate : MKMapViewDelegate
        {
            protected string annotationIdentifier = "BasicAnnotation";
            UIButton detailButton;
            MapViewController parent;
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
                    annotationView = new MKPinAnnotationView(annotation, annotationIdentifier);
                else // if we did dequeue one for reuse, assign the annotation to it
                    annotationView.Annotation = annotation;
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
                    var detailAlert = UIAlertController.Create("Annotation Clicked", "You clicked on " +
                        (annotation as MKAnnotation).Coordinate.Latitude.ToString() + ", " +
                        (annotation as MKAnnotation).Coordinate.Longitude.ToString(), UIAlertControllerStyle.Alert);
                    detailAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    parent.PresentViewController(detailAlert, true, null);
                };
                annotationView.RightCalloutAccessoryView = detailButton;
                annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromBundle("29_icon.png"));
                return annotationView;
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