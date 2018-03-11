using ViewModel;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogInPage
	{
	    public LogInPage(LogInViewModel logInViewModel)
	    {
	        BindingContext = logInViewModel;
	        InitializeComponent();
	    }
	}
}