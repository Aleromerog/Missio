using System;
using JetBrains.Annotations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Missio.NewsFeed
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsFeedPage
	{
        public NewsFeedPage()
	    {
            if(!DesignMode.IsDesignModeEnabled)
                throw new InvalidOperationException();
            BindingContext = new DummyPostsViewModel();
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