using Android.Content;
using Android.Graphics.Drawables;
using Missio.Droid.CustomRenders;
using Missio.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;

[assembly: ExportRenderer(typeof(CircleButton), typeof(CircleButtonAndroid))]

namespace Missio.Droid.CustomRenders
{
    class CircleButtonAndroid : ButtonRenderer
    {
        public CircleButtonAndroid(Context context) : base(context)
        {
        }
        

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {

            }

            if (e.OldElement != null)
            {

            }
            if(Control != null)
            {
                GradientDrawable gradientDrawable = new GradientDrawable();
                gradientDrawable.SetShape(ShapeType.Rectangle);
                gradientDrawable.SetCornerRadius(100);
                gradientDrawable.SetColor(Element.BackgroundColor.ToAndroid());
                Control.SetBackground(gradientDrawable);
            }
        }
    }
}