using Android.App;
using Android.Content.PM;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Missio.ApplicationResources;
using Missio.NewsFeed;
#if GORILLA
using UXDivers.Gorilla.Droid;
#endif
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Missio.Droid
{
    [Activity(Label = "Missio", Icon = "@drawable/ic_logo", Theme = "@style/MainTheme",MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle) 
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            #if GORILLA
            LoadApplication(Player.CreateApplication(this, new UXDivers.Gorilla.Config("Gorilla on Greco").RegisterAssembliesFromTypes<ByteArrayToImageSourceConverter, NewsFeedPostDataTemplateSelector>()));
            #else
            LoadApplication(new App());
            #endif
        }
    }
}