using Mission.Model.LocalProviders;
using ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogIn
	{
		public LogIn ()
		{
            BindingContext = new LogInViewModel(this, new NewsFeedPage(), DependencyService.Get<IUserValidator>());
			InitializeComponent ();
		}
	}
}