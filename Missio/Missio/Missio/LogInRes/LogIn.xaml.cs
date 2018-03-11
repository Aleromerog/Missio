using ViewModel;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogIn
	{
	    public LogIn(LogInViewModel logInViewModel)
	    {
	        BindingContext = logInViewModel;
	        InitializeComponent();
	    }
	}
}