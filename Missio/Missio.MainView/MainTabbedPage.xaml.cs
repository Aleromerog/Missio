using System;
using JetBrains.Annotations;
using Mission.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainTabbedPage
    {
        [Obsolete("Only for previewing with the Xamarin previewer", true)]
        public MainTabbedPage()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
	    public MainTabbedPage(MainTabbedPageViewModel mainTabbedPageViewModel, Page[] childPages)
	    {
	        foreach (var page in childPages)
	        {
                Children.Add(page);
            }

            BindingContext = mainTabbedPageViewModel;
            InitializeComponent();
	    }
	}
}