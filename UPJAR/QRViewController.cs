using Foundation;
using System;
using System.Threading;
using System.Threading.Tasks;
using UIKit;

namespace UPJAR
{
    public partial class QRViewController : UIViewController
    {
        public QRViewController (IntPtr handle) : base (handle)
        {
            RefreshDataAsync();
                   
        }

        public async void RefreshDataAsync(){
            Console.WriteLine("hello");
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var result = await scanner.Scan();
            Console.WriteLine("hello");
            if (result != null)
                Console.WriteLine("REEEe");

        }
    }
}