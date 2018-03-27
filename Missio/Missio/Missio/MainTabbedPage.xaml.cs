using System;
using JetBrains.Annotations;
using ViewModel;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainTabbedPage
	{
	    [Obsolete("Only for previewing with the Xamarin previewer", true)]
        public MainTabbedPage ()
		{
			InitializeComponent ();
		}

	    [UsedImplicitly]
	    public MainTabbedPage(MainTabbedPageViewModel mainTabbedPageViewModel, NewsFeedPage newsFeedPage)
	    {
            Children.Add(newsFeedPage);  
            BindingContext = mainTabbedPageViewModel;
            InitializeComponent();
	    }
	}
}