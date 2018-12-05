using JetBrains.Annotations;
using Xamarin.Forms.Xaml;

namespace ViewModels.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsFeedPage
	{
	    public NewsFeedPage()
	    {
            InitializeComponent();
	    }

        [UsedImplicitly]
	    public NewsFeedPage (NewsFeedViewModel newsFeedViewModel)
		{
		    BindingContext = newsFeedViewModel;
			InitializeComponent ();
		}
	}
}