using Android.Content;
using Android.Graphics.Drawables;
using Missio;
using Missio.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(ButtonLoginRender))]

namespace Missio.Droid
{

    public class ButtonLoginRender : ButtonRenderer
    {
        public ButtonLoginRender(Context context): base(context)
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
                gradientDrawable.SetCornerRadius(100.0f);
                gradientDrawable.SetColor(Element.BackgroundColor.ToAndroid());
                Control.SetBackground(gradientDrawable);
            }

        }
    }
}