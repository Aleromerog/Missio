using Android.Content;
using Android.Graphics.Drawables;
using Missio.Droid.CustomRenders;
using Missio.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;

[assembly: ExportRenderer(typeof(TransparentButtonShared), typeof(TransparentButton))]

namespace Missio.Droid.CustomRenders
{
    class TransparentButton : ButtonRenderer
    {
        public TransparentButton(Context context): base(context)
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
                gradientDrawable.SetShape(ShapeType.Line);
                gradientDrawable.SetCornerRadius(100.0f);
                gradientDrawable.SetColor(Android.Graphics.Color.Transparent);
                Control.SetBackground(gradientDrawable);
            }
        }
    }
}