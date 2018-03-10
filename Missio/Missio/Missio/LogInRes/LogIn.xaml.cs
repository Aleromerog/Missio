using Mission.Model.LocalProviders;
using ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogIn
	{
	    public LogIn()
	    {
	        var attemptToLogIn = new AttemptToLogIn(DependencyService.Get<IUserValidator>(),
	            DependencyService.Get<IDisplayAlertOnCurrentPage>(), new GoToPage(new NewsFeedPage()), DependencyService.Get<GlobalUser>());
	        BindingContext = new LogInViewModel(attemptToLogIn);
	        InitializeComponent();
	    }
	}
}