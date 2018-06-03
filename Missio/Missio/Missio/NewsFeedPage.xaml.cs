using System;
using JetBrains.Annotations;
using ViewModel;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsFeedPage
	{
        [Obsolete("Only for previewing with the Xamarin previewer", true)]
	    public NewsFeedPage()
	    {
	        App.AssertIsPreviewing();
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