using Mission.Model.LocalProviders;
using ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsFeedPage : ContentPage
	{
		public NewsFeedPage ()
		{
            BindingContext = new NewsFeedViewModel(new NewsFeedPostProvider());
			InitializeComponent ();
		}
	}
}