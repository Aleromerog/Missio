using JetBrains.Annotations;
using Xamarin.Forms.Xaml;

namespace Missio.LogIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage
    {
        public LogInPage ()
        {
            InitializeComponent ();
        }

        [UsedImplicitly]
        public LogInPage(LogInViewModel logInViewModel) : this()
        {
            BindingContext = logInViewModel;
        }
    }
}
