//using Android.Content;
//using Android.Graphics.Drawables;
//using Missio;
//using Missio.Droid;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;


//[assembly: ExportRenderer(typeof(LoginEntry), typeof(CustomEntryRendererDroid))]
//namespace Missio.Droid
//{
//    public class CustomEntryRendererDroid : EntryRenderer
//    {
//        public CustomEntryRendererDroid(Context context) : base(context)
//        {
//        }

//        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
//        {
//            base.OnElementChanged(e);
//            var gradientDrawable = new GradientDrawable();
//            gradientDrawable.SetCornerRadius(40f);
//            gradientDrawable.SetColor(Android.Graphics.Color.White);
            
//            Control?.SetBackground(gradientDrawable);

//        }
//    }
//}