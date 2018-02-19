using Mission.Model.LocalProviders;
using ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsFeedPage
	{
		public NewsFeedPage ()
		{
            BindingContext = new NewsFeedViewModel(DependencyService.Get<INewsFeedPositionProvider>());
			InitializeComponent ();
		}
	}
}