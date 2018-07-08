using System;
using JetBrains.Annotations;
using Mission.ViewModel;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainTabbedPage
    {
        [Obsolete("Only for previewing with the Xamarin previewer", true)]
        public MainTabbedPage()
        {
            App.AssertIsPreviewing();
            InitializeComponent();
        }

        [UsedImplicitly]
	    public MainTabbedPage(MainTabbedPageViewModel mainTabbedPageViewModel, NewsFeedPage newsFeedPage, ProfilePage profilePage, CalendarPage calendarPage, ToolsPage toolsPage)
	    {
            Children.Add(newsFeedPage);  
            Children.Add(toolsPage);
            Children.Add(calendarPage);
	        Children.Add(profilePage);

            BindingContext = mainTabbedPageViewModel;
            InitializeComponent();
	    }
	}
}