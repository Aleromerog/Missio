using JetBrains.Annotations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Missio.LogIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once MismatchedFileName
    public partial class LogInPage
    {
        public LogInPage ()
        {
            InitializeComponent ();
            //Background.Source = ImageSource.FromResource("Missio.LogIn.Images.LogInBackground2.png");
            //Square.Source = ImageSource.FromResource("Missio.LogIn.Images.square.png");
            //Lenguas.Source = ImageSource.FromResource("Missio.LogIn.Images.logo.png");
        }

        [UsedImplicitly]
        public LogInPage(LogInViewModel logInViewModel) : this()
        {
            BindingContext = logInViewModel;
        }
    }
}