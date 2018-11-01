using Android.App;
using Android.Content.PM;
using Android.OS;
using ButtonCircle.FormsPlugin.Droid;
using ImageCircle.Forms.Plugin.Droid;
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
            LoadApplication(new App());
        }
    }
}