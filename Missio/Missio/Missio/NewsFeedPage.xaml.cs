using ViewModel;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsFeedPage
	{
	    public NewsFeedPage (NewsFeedViewModel NewsFeedViewModel)
		{
            BindingContext = NewsFeedViewModel;
			InitializeComponent ();
		}
	}
}