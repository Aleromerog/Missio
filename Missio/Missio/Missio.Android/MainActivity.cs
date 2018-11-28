using Android.App;
using Android.Content.PM;
using Android.OS;
using ButtonCircle.FormsPlugin.Abstractions;
using ButtonCircle.FormsPlugin.Droid;
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
            ButtonCircleRenderer.Init();
            ImageCircleRenderer.Init();
            #if GORILLA
            LoadApplication(Player.CreateApplication(this, new UXDivers.Gorilla.Config("Gorilla on Greco").RegisterAssembliesFromTypes<CircleButton, ByteArrayToImageSourceConverter, NewsFeedPostDataTemplateSelector>()));
            #else
            LoadApplication(new App("https://10.0.2.2:44304/"));
            #endif
        }
    }
}