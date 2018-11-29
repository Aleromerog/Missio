using Foundation;
using UIKit;
#if GORILLA
using Missio.ApplicationResources;
using Missio.NewsFeed;
using UXDivers.Gorilla.iOS;
#endif
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
            #if GORILLA
            LoadApplication(Player.CreateApplication(new UXDivers.Gorilla.Config("Gorilla on Jorges-MacBook-Air.local").RegisterAssembliesFromTypes<ByteArrayToImageSourceConverter, NewsFeedPostDataTemplateSelector>()));
            #else
            LoadApplication(new App("https://localhost:44333/"));
            #endif
            UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(88, 3, 1);
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(88, 3, 1);
            //UITabBar.Appearance.BarTintColor = UIColor.FromRGB(88, 3, 1);
            return base.FinishedLaunching(app, options);
        }
    }
}


