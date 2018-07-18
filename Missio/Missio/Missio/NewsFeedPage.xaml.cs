using System;
using JetBrains.Annotations;
using Missio.NewsFeed;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	// ReSharper disable once MismatchedFileName
	public partial class NewsFeedPage
	{
        [Obsolete("Only for previewing with the Xamarin previewer", true)]
	    public NewsFeedPage()
	    {
            InitializeComponent();
	    }

        [UsedImplicitly]
	    public NewsFeedPage (NewsFeedViewModel<PublicationPage> newsFeedViewModel)
		{
            BindingContext = newsFeedViewModel;
			InitializeComponent ();
		}
	}
}