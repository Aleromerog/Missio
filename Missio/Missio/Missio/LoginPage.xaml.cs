using JetBrains.Annotations;
using Missio.LogIn;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace Missio.LogInRes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage
    {
        public LogInPage ()
        {
            InitializeComponent ();
            Background.Source = ImageSource.FromResource("Missio.Images.LogInBackground2.png");
            Square.Source = ImageSource.FromResource("Missio.Images.square.png");
            Lenguas.Source = ImageSource.FromResource("Missio.Images.logo.png");
        }

        [UsedImplicitly]
        public LogInPage(LogInViewModel<RegistrationPage> logInViewModel) : this()
        {
            BindingContext = logInViewModel;
        }
    }
}