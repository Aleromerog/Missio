using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace Missio.LogInRes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewLoginPage
    {
        public NewLoginPage ()
        {
            InitializeComponent ();
            background.Source = ImageSource.FromResource("Missio.Images.LogInBackground2.png");
            square.Source = ImageSource.FromResource("Missio.Images.square.png");
            lenguas.Source = ImageSource.FromResource("Missio.Images.logo.png");
            

        }
    }
}