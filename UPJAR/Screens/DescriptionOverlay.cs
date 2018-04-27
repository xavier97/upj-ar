using System;
using UIKit;
using CoreGraphics;

public class DescriptionOverlay : UIView
{
    // control declarations

    UITextView loadingLabel;

    public DescriptionOverlay(CGRect frame, string desc) : base(frame)
    {
        // configurable bits
        BackgroundColor = UIColor.White;
       
        AutoresizingMask = UIViewAutoresizing.All;

        nfloat labelHeight = 80;
        nfloat labelWidth = Frame.Width - 30;

        // derive the center x and y
        nfloat centerX = Frame.Width / 2;
        nfloat centerY = Frame.Height / 2;


        // create and configure the "Loading Data" label
        loadingLabel = new UITextView(new CGRect(
            centerX - (labelWidth / 2),
            centerY + 20,
            labelWidth,
            labelHeight
            ));

        loadingLabel.TextColor = UIColor.Black;
        loadingLabel.Text = desc;

        loadingLabel.TextAlignment = UITextAlignment.Center;
        loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
        AddSubview(loadingLabel);
    }

    /// <summary>
    /// Fades out the control and then removes it from the super view
    /// </summary>
    public void Hide()
    {
        UIView.Animate(
            0.0, // duration
            () => { Alpha = 0; },
            () => { RemoveFromSuperview(); }
        );
    }
}
