using Missio.LogIn;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace Missio.LogInRes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage
    {
        public LogInViewModel LogInViewModel { get; set; }

        public LogInPage ()
        {
            InitializeComponent ();
            background.Source = ImageSource.FromResource("Missio.Images.LogInBackground2.png");
            square.Source = ImageSource.FromResource("Missio.Images.square.png");
            lenguas.Source = ImageSource.FromResource("Missio.Images.logo.png");
        }

        public LogInPage(LogInViewModel logInViewModel) : this()
        {
            LogInViewModel = logInViewModel;
        }
    }
}