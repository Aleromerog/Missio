using ButtonCircle.FormsPlugin.iOS;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Missio.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            LoadApplication(new App());

            ButtonCircleRenderer.Init();
            ImageCircleRenderer.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}


