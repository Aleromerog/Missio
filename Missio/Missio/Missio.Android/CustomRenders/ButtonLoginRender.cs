using Android.Content;
using Android.Graphics.Drawables;
using Missio.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(ButtonLoginRender))]

namespace Missio.Droid
{
    public class ButtonLoginRender : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        public ButtonLoginRender(Context context): base(context)
        {
            
        } 
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var boton = new GradientDrawable();
            boton.SetCornerRadius(40f);
            boton.SetColor(Android.Graphics.Color.Snow);
        }
    }

}